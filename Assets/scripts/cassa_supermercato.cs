using System.Collections;
using System.Collections.Generic;
using BurgerDomain;
using TMPro;
using UnityEngine;

public class CassaSupermercato : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> oggetti_da_comprare;
    public float totale_da_pagare = 0f;
    public string testo_cassa;
    public float denaro_player;
    public TextMeshPro testoCassa;
    
    
    void Start()
    {
        denaro_player = FindObjectOfType<Player>().Money;
        testoCassa = GetComponentInChildren<TextMeshPro>();
        for (int i = 0; i < oggetti_da_comprare.Count; i++)
        {
            totale_da_pagare= totale_da_pagare+oggetti_da_comprare[i].GetComponent<IngredientInfo>().Prize*oggetti_da_comprare[i].GetComponent<IngredientInfo>().Quantity;
        }

        testo_cassa = "denaro:" + denaro_player + "\n\ntotale da pagare :" + totale_da_pagare;
        testoCassa.text=testo_cassa;
    }
    void RimuoviOggettoDaComprare(GameObject oggetto)
    {
        if (oggetti_da_comprare.Contains(oggetto))
        {
            oggetti_da_comprare.Remove(oggetto);
            AggiornaTotaleDaPagare(); // Aggiorna il totale dopo la rimozione
        }
        //FindObjectOfType<Player>().GetComponent<Player>().ConvertList();
        
    }

    // Funzione chiamata quando si preme un pulsante o si verifica un'azione per rimuovere un oggetto
    public void RimuoviOggettoCorrente()
    {
        // Assumi che il primo oggetto nella lista debba essere rimosso, puoi adattare questa logica in base alle tue esigenze
        if (oggetti_da_comprare.Count > 0)
        {
            GameObject oggettoDaRimuovere = oggetti_da_comprare[0];
            RimuoviOggettoDaComprare(oggettoDaRimuovere);
        }
    }
    public void AggiornaTotaleDaPagare()
    {
        totale_da_pagare = 0;
        denaro_player = FindObjectOfType<Player>().Money;
        // Aggiorna il totale da pagare in base agli oggetti nella lista
        for (int i = 0; i < oggetti_da_comprare.Count; i++)
        {
            if (oggetti_da_comprare[i].GetComponent<IngredientInfo>().Quantity > 0)
            {
                totale_da_pagare += oggetti_da_comprare[i].GetComponent<IngredientInfo>().Prize *
                                    oggetti_da_comprare[i].GetComponent<IngredientInfo>().Quantity;
            }
            else
            {
                oggetti_da_comprare.RemoveAt(i);
            }
        }
        testo_cassa = "denaro:" + denaro_player + "\ntotale da pagare :" + totale_da_pagare;
        testoCassa.text=testo_cassa;
    }


    // Update is called once per frame
    /*void Update()
    {
        //aggiorna testo e per soldi e totale da pagare
        //denaro_player = FindObjectOfType<Player>().Money;
        //AggiornaTotaleDaPagare();
    }*/
}
