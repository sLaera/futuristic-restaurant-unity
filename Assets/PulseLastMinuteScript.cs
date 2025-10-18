using BurgerDomain;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PulseLastMinuteScript : MonoBehaviour
{
    LastMinuteScreen scriptScreen;
    public bool again = false;
    public AudioSource audioSource;
    public AudioClip[] list;
    // Start is called before the first frame update
    void Start()
    {
        scriptScreen = GameObject.FindGameObjectWithTag("LastMinuteScreen").GetComponent<LastMinuteScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica se il tasto sinistro del mouse � stato premuto
        if (Input.GetMouseButtonDown(0) && scriptScreen.pulse==true)
        {
 
            // Ottieni un raggio dalla posizione del mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verifica se il raggio colpisce un oggetto
            if (Physics.Raycast(ray, out hit))
            {
                // Verifica se l'oggetto colpito � quello che vuoi gestire
                if (hit.collider.gameObject == this.gameObject)
                {
                    
                    // Esegui l'azione desiderata quando l'oggetto � cliccato
                    if (again == false)
                    {
                        audioSource.clip = list[0];
                        audioSource.Play();
                        scriptScreen.TestoLastMinute.text = "PER CONFERMARE IL PAGAMENTO DI $" + scriptScreen.element.Prize * 2 +" CLICCA NUOVAMENTE IL PULSANTE";
                        again = true;
                    }
                    else
                    {
                        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Money >= scriptScreen.element.Prize*2) {
                            scriptScreen.TestoLastMinute.text = "HAI COMPLETATO L'ACQUISTO DI 1 " + scriptScreen.element.Type.ToString().ToUpper() + "!";
                            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Money -= scriptScreen.element.Prize * 2 ;
                            GameManager.Instance.oldMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Money - scriptScreen.element.Prize*2;
                            audioSource.clip = list[1];
                            audioSource.Play();
                            // GameObject.FindGameObjectWithTag("mieingred").GetComponent<riepilogo_ingredienti_in_possesso>().riepilogo();
                            List<IngredientInfo> allingredients = GameManager.Instance.AllPossibleIngredients;
                            for (int j = 0; j < allingredients.Count; j++)
                            {
                                if (allingredients[j].Type ==
                                    scriptScreen.element.Type)
                                {
                                    IngredientInfo copia;
                                    copia = allingredients[j];
                                    copia.uid = Guid.NewGuid();
                                    copia.Quantity = 1;
                                    copia.Prize = scriptScreen.element.Prize;
                                    FindObjectOfType<Player>().GetComponent<Player>().CurrentOwnedIngredients.Add(copia);

                                }
                            }
                            GameObject.FindGameObjectWithTag("start").GetComponent<StartCooking>().update_table();
                            foreach (var gestioneTesto in FindObjectsOfType<GestioneTesto>(true))
                            {
                                gestioneTesto.gameObject.SetActive(true);
                            }
                            GameManager.Instance.Listaqta.Add(1);
                            GameManager.Instance.ListaStringhe.Add(scriptScreen.element.Type);
                            GameManager.Instance.Listaprezzi.Add(scriptScreen.element.Prize);
                            Debug.Log(scriptScreen.element.Type);
                            again = false;
                            scriptScreen.pulse = false;
                            scriptScreen.fine = false;
                        }
                        else
                        {
                            again = false;
                            scriptScreen.pulse = false;
                            scriptScreen.fine = false;
                            scriptScreen.TestoLastMinute.text = "NON HAI ABBASTANZA SOLDI PER COMPLETARE L'ACQUISTO!";
                        }
                    }
                }
            }
        }
    }
}
