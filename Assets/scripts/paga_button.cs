using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BurgerDomain;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PagaButton : MonoBehaviour
{
    // Start is called before the first frame update
    public bool pagamento_state=true;
    private int cnt;
    public AudioSource audioSource;
    public AudioClip[] list;
    public void Start()
    {
        pagamento_state = false;
        cnt = 0;
        audioSource = GetComponent<AudioSource>();
        
    }

    public void Buy()
    {
        float pagamento = GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>()
            .totale_da_pagare;
        float money = FindObjectOfType<Player>().Money;
        //toggle ad ogni chiamata
        if (money >= pagamento)
        {
            GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>()
                .AggiornaTotaleDaPagare();
            pagamento_state = !pagamento_state;

            if (pagamento_state == true)
            {
                GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>().testoCassa
                    .text += "\n per confermare il pagamento cliccare ancora";
                audioSource.clip = list[0];
                audioSource.Play();
            }

            if (pagamento_state == false)
            {
                // sto aspettando il click del button prima volta
                GameObject.FindGameObjectWithTag("skip_button").GetComponent<SkipButton>().index = 0;
                if (money >= pagamento)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Money = money - pagamento;
                    GameManager.Instance.oldMoney = money - pagamento;
                    GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>()
                            .totale_da_pagare =
                        0;
                    List<GameObject> l = GameObject.FindGameObjectWithTag("cassa_supermercato")
                        .GetComponent<CassaSupermercato>().oggetti_da_comprare;
                    List<GameObject> oggetti_da_inserire = new List<GameObject>();
                    //GameManager.Instance.ListaStringhe.Clear();
                    //GameManager.Instance.Listaprezzi.Clear();
                    //GameManager.Instance.Listaqta.Clear();
                    for (int i = 0; i < l.Count; i++)
                    {
                        // devo aver rimosso il physics grabbable del supermercato e aver inserito il grabbable del ristorante
                        l[i].AddComponent<PhysicsGrabbable>();
                        l[i].GetComponent<PhysicsGrabbableSupermarket>().enabled = false; // meglio elimminarlo
                        Destroy(l[i].GetComponent<PhysicsGrabbableSupermarket>());
                        // ora ogni oggetto è pronto ad essere inserito negli owned ingredients
                        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CurrentOwnedIngredients.Add(l[i]);
                        GameManager.Instance.Listaprezzi.Add(l[i].GetComponent<IngredientInfo>().Prize);
                        GameManager.Instance.Listaqta.Add(l[i].GetComponent<IngredientInfo>().Quantity);
                        GameManager.Instance.ListaStringhe.Add(l[i].GetComponent<IngredientInfo>().Type);
                    }
                    audioSource.clip = list[1];
                    audioSource.Play();
                    GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>()
                        .oggetti_da_comprare
                        .Clear();

                    // liste aggiornate
                    GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>()
                        .AggiornaTotaleDaPagare();
                    GameObject.FindGameObjectWithTag("testo_qta").GetComponent<show_qta>().Update_text();
                    GameObject.FindGameObjectWithTag("mieingred").GetComponent<riepilogo_ingredienti_in_possesso>().Riepilogo();
                }
                
            }
        }else
        {
            GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>()
                .AggiornaTotaleDaPagare();
            GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>().testoCassa
                .text += "\n denaro Insufficente!";

        }
    }

    // Update is called once per frame

}
