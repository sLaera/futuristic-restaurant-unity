using System;
using System.Collections;
using System.Collections.Generic;
using BurgerDomain;
using BurgerDomain.Classes;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
using Unity.VisualScripting;


public enum RobotStatus
{
    Created,     //quando viene creato o quando finisce tutto il ciclo viene settato a questo
    StartOrder,  //appena arriva un ordine lo stato diventa startOrder. il robot inizia a camminare
    FinishOrder, //l'ordine è stato completato. e' stato mostrato a schermo se e' corretto o meno e il robot puo' iniziare ad uscire
    Waiting
}

public class RobotCameriereScript : MonoBehaviour
{
    [FormerlySerializedAs("mAnimator")]
    public Animator MAnimator;
    [FormerlySerializedAs("theDestination")]
    public List<GameObject> ToKitchenDestinations;
    public int _positionNumber = 0;
    public Order _order;
    [FormerlySerializedAs("_status")]
    public RobotStatus Status;
    private Creazionepanino _plate;
    private GameObject Robot;
    private List<string> stringa;
    //Set these Textures in the Inspector
    public Texture HappyScreen;
    public Texture AngryScreen;
    public Texture WaitingScreen;
    public Material ScreenMaterial;
    public int SchermoSel;  // 0 = happy, 1 = angry, 2 = niente

    
    
    [SerializeField] private TextMeshPro TestoASchermo;
    private string _testoPaninoIncompleto;
    private string _testoPassaggio;
    LastMinuteScreen pulsescript;
    // Start is called before the first frame update
    void Start()
    {
        MAnimator = GetComponent<Animator>();
        _order = null;
        ScreenMaterial.mainTexture = HappyScreen;
        MAnimator.ResetTrigger("TriggerWalking");
        MAnimator.SetTrigger("TriggerIdle");
        Status = RobotStatus.Created;
        GameManager.OnRaycastInteract += OnBellRightClick;
        _plate = FindObjectOfType<Creazionepanino>();
        _testoPaninoIncompleto = "PANINO INCOMPLETO, INSERIRE INGREDIENTI MANCANTI";
        TestoASchermo.text = "";
        pulsescript = GameObject.FindGameObjectWithTag("LastMinuteScreen").GetComponent<LastMinuteScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //--timeleft dell'ordine---
        if (_order != null && _order.Status == OrderStatus.InProgress)
        {
            
            _order.TimeLeft -= Time.deltaTime;

            if (_order.TimeLeft <= 0)
            {
                
              
              FinishOrder(OrderResult.Expired);
              pulsescript.pulse = false;
                
            }
        }
        
        switch (Status)
        {
            case RobotStatus.StartOrder:
                var destination = ToKitchenDestinations[_positionNumber];
                
                bool stessaPosizione = Mathf.Abs(transform.position.x - destination.transform.position.x) < 0.01 &&
                                       Mathf.Abs(transform.position.z - destination.transform.position.z) < 0.01;
               
                if (!stessaPosizione)
                {
                    //vado verso la destinazione
                    MAnimator.ResetTrigger("TriggerIdle");
                    MAnimator.SetTrigger("TriggerWalking");
                   
                    transform.LookAt(destination.transform);
                    // Ottieni il vettore forward attuale del GameObject
                    Vector3 forward = transform.forward;

                    // Applica una rotazione di 45 gradi attorno all'asse Y
                    Quaternion rotation = Quaternion.Euler(0, 90, 0);
                    forward = rotation * forward;

                    // Assegna il nuovo forward vector al GameObject
                    transform.forward = forward;
                    transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, 0.2f);
                }
                else
                {
                    //ciclo per tutte le posizioni fin quando non arrivo in cucina
                    _positionNumber++;
                    if (_positionNumber == 4)
                    {
                        ScreenMaterial.mainTexture = HappyScreen;
                        //animazione porta che si chiude
                    }

                    if (_positionNumber == 3)
                    {
                    } //animazione porta che si apre

                    if (_positionNumber == 1 && _order.TimeLeft > 0)
                    {
                        pulsescript.fine = false;
                        MAnimator.ResetTrigger("TriggerWalking");
                        MAnimator.SetTrigger("TriggerIdle");
                        ScreenMaterial.mainTexture = WaitingScreen;
                        
                        //<b><color=#406AF5>Cheese Burger:</color></b>

                        // <color=#B2A75A>bread</color>
                        //     cheese
                        // meat
                        // salad
                        //     <color=#B2A75A>bread</color>
                        TestoASchermo.text = $"<b><color=#406AF5>{_order.Name}</color></b>\n<color=#58522B>bread</color>\n";
                        foreach (var ingredient in _order.Burger.ingredients)
                        {
                            var element = ingredient.GetComponent<IngredientInfo>();
                            TestoASchermo.text += element.Type.ToString().ToUpper() + "\n";
                        }
                        TestoASchermo.text += $"<color=#58522B>bread</color>";
                       
                        _order.Status = OrderStatus.InProgress;
                        Status = RobotStatus.Waiting;
                    }
                    if (_positionNumber == ToKitchenDestinations.Count)
                    {
                        _positionNumber = 0;
                        transform.Rotate(0, -90, 0);
                        MAnimator.ResetTrigger("TriggerWalking");
                        MAnimator.SetTrigger("TriggerIdle");
                        Status = RobotStatus.Waiting;
                        //IL ROBOT E' ARRIVATO IN CUCINA
                        if (_order == null || _order.Status == OrderStatus.Finished)
                        {
                            GameManager.Instance.GenerateNextOrder();
                        }
                    }
                }
                break;
            case RobotStatus.FinishOrder:
                //todo: esci dalla cucina
                //todo: quando è fuori fai _status = RobotStatus.Created;
                break;
        }
    }
    
    public void StartOrder(Order order)
    {
        _order = order;
        Status = RobotStatus.StartOrder;
    }
    
    /// <summary>
    /// Funzione chiamata nel momento in cui un ordine è finito. L'ordine può essere corretto o incorretto
    /// </summary>
    /// <param name="result"></param>
    private void FinishOrder(OrderResult result)
    {
        TestoASchermo.text = "";
        _order.Status = OrderStatus.Finished;
        switch (result)
        {
            case OrderResult.Expired:
                //il tempo è finito prima di aver dato il panino. si perdono i soldi dell'ordine
                TestoASchermo.text = "TEMPO SCADUTO!\n-$" + _order.Burger.GetTotalCost()  * 0.8f;
                SchermoSel = 1;
                GameObject.FindGameObjectWithTag("riepilogo_ordini").GetComponent<riepilogo_ordini>().ordini_errati++;
                GameObject.FindGameObjectWithTag("riepilogo_ordini").GetComponent<riepilogo_ordini>().ordini_tot++;
                GameObject.FindGameObjectWithTag("riepilogo_ordini").GetComponent<riepilogo_ordini>().Update_testo();
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SubMoney(_order.Burger.GetTotalCost() * 0.8f);
                GameManager.Instance.oldMoney -= _order.Burger.GetTotalCost() * 0.8f;
                if (GameManager.Instance.oldMoney < 0)
                {
                    GameManager.Instance.oldMoney = 0;
                }
            //    Debug.Log($"Expired, cost:{_order.Burger.GetTotalCost()}");
                break;
            case OrderResult.Incorrect:
                //il burger non è correto. l'utente perde soldi
                TestoASchermo.text = "ORDINE SBAGLIATO!\n-$" + _order.Burger.GetTotalCost() * 0.8f;
                SchermoSel = 1;
                GameObject.FindGameObjectWithTag("riepilogo_ordini").GetComponent<riepilogo_ordini>().ordini_errati++;
                GameObject.FindGameObjectWithTag("riepilogo_ordini").GetComponent<riepilogo_ordini>().ordini_tot++;
                GameObject.FindGameObjectWithTag("riepilogo_ordini").GetComponent<riepilogo_ordini>().Update_testo();
                GameManager.Instance.Player.SubMoney(_order.Burger.GetTotalCost() * 0.8f);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SubMoney(_order.Burger.GetTotalCost() * 0.8f);
                GameManager.Instance.oldMoney -= _order.Burger.GetTotalCost() * 0.8f;
                if (GameManager.Instance.oldMoney < 0)
                {
                    GameManager.Instance.oldMoney = 0;
                }
             //   Debug.Log($"Incorrect, cost:{_order.Burger.GetTotalCost()}");
                break;
            case OrderResult.Correct:
                //il burger è corretto allora l'utente guadagna soldi
                TestoASchermo.text = "ORDINE CORRETTO!\n+$"+_order.Burger.GetTotalCost() * 1.20f;
                SchermoSel = 0;
                GameObject.FindGameObjectWithTag("riepilogo_ordini").GetComponent<riepilogo_ordini>().ordini_corretti++;
                GameObject.FindGameObjectWithTag("riepilogo_ordini").GetComponent<riepilogo_ordini>().ordini_tot++;
                GameObject.FindGameObjectWithTag("riepilogo_ordini").GetComponent<riepilogo_ordini>().Update_testo();
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddMoney(_order.Burger.GetTotalCost() * 1.2f);
                GameManager.Instance.oldMoney += _order.Burger.GetTotalCost() * 1.2f;
             //   Debug.Log($"Correct, cost:{_order.Burger.GetTotalCost()}");
                break;
        }

        Invoke(nameof(AttesaRisultato), 3);

        
        //---pulizia panino----
        Creazionepanino piatto = FindObjectOfType<Creazionepanino>();
        List<GameObject> l= piatto.ListaIngredienti;
        // lista da passare per controllo ordini
        //if ordine corretto
        for (int i = 0; i < l.Count; i++)
        {
            Destroy(l[i]);
        }
        piatto.ListaIngredienti.Clear();
        
        foreach (var macchiaSalsa in FindObjectsOfType<MacchiaSalsa>())
        {
            Destroy(macchiaSalsa.gameObject);
        }
        //---------------------
        
        //Status = RobotStatus.StartOrder;
    }

    private void OnBellRightClick(RaycastInteractable interactable)
    {
        if (interactable.CompareTag("campanello") && Status == RobotStatus.Waiting)
        {
            //è stato cliccato il campanello e il robot sta aspettando l'ordine.
            //prendo il piatto e controllo che sia stato fatto bene
            var burger = _plate.GetBurger();
            if (_plate.GetBurger().IsUnityNull() == false)
            {
                if (burger.IsComplete())
                {
                    FinishOrder(_order.CompareBurgerWithOrder(burger) ? OrderResult.Correct : OrderResult.Incorrect);
                }
                else
                {
                    if (_order.TimeLeft > 2)
                    {
                        _testoPassaggio = TestoASchermo.text;
                        TestoASchermo.text = _testoPaninoIncompleto;
                        Invoke(nameof(AttesaSchermo), 2);
                    }
                } 
            }
        }
    }
    private void AttesaSchermo()
    {
        TestoASchermo.text = _testoPassaggio;
    }

    private void AttesaRisultato()
    {
        TestoASchermo.text = "";
        if (SchermoSel == 0) { ScreenMaterial.mainTexture = HappyScreen; }
        if (SchermoSel == 1) { ScreenMaterial.mainTexture = AngryScreen; }
        Invoke(nameof(AttesaRestart), 2);
    }

    private void AttesaRestart()
    {
        Status = RobotStatus.StartOrder;
    }
}
