using System.Collections;
using System.Collections.Generic;
using BurgerDomain;
using TMPro;
using UnityEngine;

public class show_qta : MonoBehaviour
{
    public TextMeshPro Testo;
    // Start is called before the first frame update
    void Start()
    {
        Testo = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    public void Update_text()
    {
        Testo.text = "Cesto acquisti:\nIngrediente    Quantità\n";
        int index = GameObject.FindGameObjectWithTag("skip_button").GetComponent<SkipButton>().index;
        List<GameObject> list = GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>()
            .oggetti_da_comprare;
        for(int i=0; i<list.Count;i++)
        {
            if (list[i].GetComponent<IngredientInfo>().Quantity > 0)
            {
                if (i == index)
                {
                    Testo.text += "<color=red>" + list[i].GetComponent<IngredientInfo>().Type + "             " +
                                  list[i].GetComponent<IngredientInfo>().Quantity + "\n<color=black></color>";
                }
                else
                {
                    Testo.text += "<color=black>" + list[i].GetComponent<IngredientInfo>().Type + "             " +
                                  list[i].GetComponent<IngredientInfo>().Quantity + "\n<color=red></color>";
                }
            }
        }
        Testo.text += "<color=black>Totale da pagare: " +GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>().totale_da_pagare+"<color=black></color>";
    }
}
