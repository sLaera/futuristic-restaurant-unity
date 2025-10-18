using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BurgerDomain;
using BurgerDomain.Classes;
using Data;
using DG.Tweening;
using Enums;
using Newtonsoft.Json;
using UI;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Utils;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static event Action<RaycastInteractable> OnRaycastInteract;
    
    public static GameManager Instance { get; private set; }

    private GameplayState _gameplayState;
    
    private List<string> _scenesToLoad;
    private List<string> _scenesToUnload;

    public Player Player { set; get;}
    public Vector3 old_pos ;
    public int n_ordini_corretti;

    
    //----------------------------------------------------------------------
    // [Header("Tips data")]
    // [SerializeField] private TextAsset TipsData;
    public TipsReader.TipsCollection Tips { private set; get; }
    
    [SerializeField]
    [SerializeAs("Fattore di probabilità per ingredienti con quantità 0. Valore tra [0,1]")]
    private float ProbabilityOfRunOutIngredients = 1;

    [SerializeField] [SerializeAs("Lista di tutti i possibili ingredienti che appaiono nel gioco")]
    public List<IngredientInfo> AllPossibleIngredients;

    
    [FormerlySerializedAs("Menu")]
    public List<MenuBurgerItem> MenuBurger;
    
    //-----------------times of orders-------------------------------------
    [Header("_________________________________________________")]
    //-------ttl-----------
    [SerializeField]
    private float TimeToLifeOrderStart = 9f;
    [SerializeField]
    private float TimeToLifeOrderDecrease = 0.5f;
    [SerializeField]
    private float TimeToLifeOrderMinimum = 3f;
    private float _timeToLifeOrder;
    //-----------------------
    //------between-----------
    [SerializeField]
    private float TimeBetweenOrdersStart = 10;
    [SerializeField]
    private float TimeBetweenOrdersDecrease = 0.5f;
    [SerializeField]
    private float TimeBetweenOrdersMinimum = 5f;
    private float _timeBetweenOrders;
    //-------------------------------------------------------------------------

    //--------------------number of ingredeints in burger----------------
    [SerializeField]
    private float NumberOfIngredientsStart = 2;
    [SerializeField]
    private float NumberOfIngredientsIncrease = 0.5f;
    [FormerlySerializedAs("NumberOfIngredientsIncreaseMaximum")]
    [SerializeField]
    private float NumberOfIngredientsMaximum = 10;
    private float _numberOfIngredients;
    public float oldMoney=100f;
    public List<IngredientInfo> oldList;
    public List<IngredientInfo> LeftList;
    public List<IngredientType> ListaStringhe;
    public List<int> Listaqta;
    public List<float> Listaprezzi;
    //leftingredients
    public List<IngredientType> ListaStringheLeft;
    public List<int> ListaqtaLeft;
    public List<float> ListaprezziLeft;
    public bool intosupermarket=false;
    public bool intorestaurant=false;
    public int cameraforw=0;
    
    
    //------------------------------------------------------
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //old_pos = FindObjectOfType<Player>().transform.position;
        _timeToLifeOrder = TimeToLifeOrderStart;
        _timeBetweenOrders = TimeBetweenOrdersStart;
        Player = FindObjectOfType<Player>();
        //Tips = DataAssetLoader.Load<Tips>(TipsData);
        Tips = TipsReader.GetTips();
        //start
        oldMoney = 100;
        //tolgo il commento per la build
        SceneManager.LoadScene("start_menu", LoadSceneMode.Additive);
    }
    


    public void LoadUnloadScenes(List<string> scenesToLoad, List<string> scenesToUnload)
    {
        FindObjectOfType<loadingpanel>(true).gameObject.SetActive(true);
        _scenesToLoad = scenesToLoad;
        _scenesToUnload = scenesToUnload;
        Invoke(nameof(LoadUnload), 0.3f);
    }

    private void LoadUnload()
    {
        //ho dati del player della scena che verrà scaricata
        foreach (var scene  in _scenesToLoad)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
        FindObjectOfType<loadingpanel>(true).gameObject.SetActive(true);
        Player = FindObjectOfType<Player>();
        foreach (var scene in _scenesToUnload)
        {
            SceneManager.UnloadSceneAsync(scene);
        }
    }

    public void ConvertList(List<IngredientInfo> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < AllPossibleIngredients.Count; j++)
            {
                if (AllPossibleIngredients[j].Type ==
                    list[i].Type)
                {
                    IngredientInfo copia;
                    copia = AllPossibleIngredients[j];
                    //copia.uid = Guid.NewGuid();
                    copia.Quantity=list[i].Quantity;
                    copia.Prize=list[i].Prize;
                    oldList.Add(copia);
                    //copia.SetActive(false);
                }
            }

        }
    }
    
    public void ConvertListLeft(List<IngredientInfo> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < AllPossibleIngredients.Count; j++)
            {
                if (AllPossibleIngredients[j].Type ==
                    list[i].Type)
                {
                    IngredientInfo copia;
                    copia = AllPossibleIngredients[j];
                    //copia.uid = Guid.NewGuid();
                    copia.Quantity=list[i].Quantity;
                    copia.Prize=list[i].Prize;
                    LeftList.Add(copia);
                    ListaprezziLeft.Add(list[i].Prize);
                    ListaStringheLeft.Add(list[i].Type);
                    ListaqtaLeft.Add(list[i].Quantity);
                    Debug.Log("aggiunto "+list[i].Type);
                    //copia.SetActive(false);
                    
                }
            }

        }
    }

    public void StartOrderGeneration()
    {
        _timeToLifeOrder = TimeToLifeOrderStart;
        _timeBetweenOrders = TimeBetweenOrdersStart;
        _numberOfIngredients = NumberOfIngredientsStart;
        _gameplayState = GameplayState.OrderGenerationBegin;
        GenerateNextOrder();
    }

    public void StopOrderGeneration()
    {
        //CancelInvoke();
        _gameplayState = GameplayState.OrderGenerationEnd;
        
        //QUI I COURRENT OWNED INGREDIENTS DEL PLAYER DEVONO ESSERE AGGIORNATI
        // QUINDI GLI INGREDIENTI USATI DEVONO ESSERE GIà SPARITI 
        
        // mantengo la coerenza aggiornando le liste
        List<IngredientInfo> l = GameObject.FindObjectOfType<Player>().CurrentOwnedIngredients;
        List<int> ints = GameManager.Instance.Listaqta;
        List<float> floats= GameManager.Instance.Listaprezzi;
        List<IngredientType> types= GameManager.Instance.ListaStringhe;
        // devo azzerare tutte le liste cosi poi le ricompongo
        ListaStringhe.Clear();
        Listaqta.Clear();
        Listaprezzi.Clear();
        for (int i = 0; i < l.Count; i++)
        {
            if (l[i].Quantity > 0)
            {
                ListaStringhe.Add(l[i].Type);
                Listaqta.Add(l[i].Quantity);
                Listaprezzi.Add(l[i].Prize);
            }
        }
        // ho così liste aggiornate
    }

    public void FinishTurn()
    {
        Player = FindObjectOfType<Player>();
        //il turno finisce
        StopOrderGeneration();
        var finishPositin = FindObjectOfType<FinishTurnPosition>().transform.position;
        var position = new Vector3(finishPositin.x,Player.transform.position.y,finishPositin.z);
        Player.transform.DOMove(position,2).onComplete += () => {
            var fpsController = Player.GetComponent<FirstPersonController>();
            fpsController.playerCanMove = true;
            fpsController.enableJump = true;
            fpsController.enableCrouch = true;
            fpsController.enableHeadBob = true;
            fpsController.is_cooking = false;
        };
    }

    public void GenerateNextOrder()
    {
        Player = FindObjectOfType<Player>();
        if (Player.CurrentOwnedIngredients.Count <= 0 || 
            Player.CurrentOwnedIngredients.Aggregate(0,(sum,i)=> sum + i.Quantity) <= 0)
        {
            FinishTurn();
        }
        if (_gameplayState == GameplayState.OrderGenerationBegin)
        {
            _numberOfIngredients = Math.Min(_numberOfIngredients, Player.CurrentOwnedIngredients.Count); //non ci possono essere più ingredienti di quanti non ne abbia l'utnete
            //se non hai soldi gli ordini diventano facilissimi
            if (Player.Money < 5)
            {
                _numberOfIngredients = 1;
            }
            Order order = OrderManager.GenerateNewOrder(MenuBurger, _timeToLifeOrder);
            //il prossimo ordine sarà generato prima con un tempo per farlo minore e con più ingredienti
            //----
            _timeBetweenOrders -= TimeBetweenOrdersDecrease;
            _timeBetweenOrders = Math.Max(_timeBetweenOrders, TimeBetweenOrdersMinimum);
            //----
            _timeToLifeOrder -= TimeToLifeOrderDecrease;
            _timeToLifeOrder = Math.Max(_timeToLifeOrder, TimeToLifeOrderMinimum);
            //----
            _numberOfIngredients += NumberOfIngredientsIncrease;
            _numberOfIngredients = Math.Min(_numberOfIngredients, NumberOfIngredientsMaximum);
            //----
            //Invoke(nameof(GenerateNextOrder), _timeBetweenOrders);
            
            //---Creare un nuovo robot con l'ordine assegnato
            var robot = FindObjectOfType<RobotCameriereScript>();
            robot.StartOrder(order);
        }
    }
    public static void InvokeRaycastInteract(RaycastInteractable obj)
    {
        OnRaycastInteract?.Invoke(obj);
    }
}

// [System.Serializable]
// public class Tips: IEnumerable<Tip>
// {
//     public List<Tip> tips;
//     
//     public Tip GetById(int id)
//     {
//         return tips.Find( t => t.id == id);
//     }
//     
//     public Tip GetRandomTip()
//     {
//         var id = Random.Range(0,tips.Count);
//         return tips[id];
//     }
//     
//     public IEnumerator<Tip> GetEnumerator()
//     {
//         return tips.GetEnumerator();
//     }
//
//     IEnumerator IEnumerable.GetEnumerator()
//     {
//         return GetEnumerator();
//     }
// }
//
// [System.Serializable]
// public class Tip
// {
//     [JsonProperty("id")]
//     public int id { set; get; }
//     [JsonProperty("title")]
//     public string title { set; get; }
//     [JsonProperty("description")]
//     public string description { set; get; }
// }
