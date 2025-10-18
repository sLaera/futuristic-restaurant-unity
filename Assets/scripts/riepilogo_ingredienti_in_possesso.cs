using System;
using System.Collections;
using System.Collections.Generic;
using BurgerDomain.Classes;
using TMPro;
using UnityEngine;

public class riepilogo_ingredienti_in_possesso : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro testo_ordine;

    private void Start()
    {
        Riepilogo();
    }

    // Update is called once per frame
    public void Riepilogo ()
    {
        int meat = 0;
        int cheese = 0;
        int salad = 0;
        int tomato = 0;
        int salami = 0;
        int ham = 0;
        int onion = 0;
        if (GameManager.Instance.ListaStringhe.Count > 0)
        {
            List<IngredientType> stringhe = GameManager.Instance.ListaStringhe;
            List<int> q = GameManager.Instance.Listaqta;
            for (int i = 0; i < stringhe.Count; i++)
            {
                if (stringhe[i] == IngredientType.Cheese)
                {
                    cheese += q[i];
                }

                if (stringhe[i] == IngredientType.Ham)
                {
                    ham += q[i];
                }

                if (stringhe[i] == IngredientType.Tomato)
                {
                    tomato += q[i];
                }

                if (stringhe[i] == IngredientType.Onion)
                {
                    onion += q[i];
                }

                if (stringhe[i] == IngredientType.Salad)
                {
                    salad += q[i];
                }

                if (stringhe[i] == IngredientType.Salami)
                {
                    salami += q[i];
                }

                if (stringhe[i] == IngredientType.Meat)
                {
                    meat += q[i];
                }
            }
        }

        
        /*
        string testo = I miei ingredienti:\ningrediente\nProsciutto\nInsalata\nSalame\nPomodoro\nHamburger\nCipolla\nFormaggio\n;
         */
        string testo = 
                       "\n" + "<align=right>Quantità</align>\n" +
                       $"<align=right>{ham}</align>\n" +
                       $"<align=right>{salad}</align>\n" +
                       $"<align=right>{salami}</align>\n" +
                       $"<align=right>{tomato}</align>\n" +
                       $"<align=right>{meat}</align>\n" +
                       $"<align=right>{onion}</align>\n" +
                       $"<align=right>{cheese}</align>\n";
        testo_ordine.text = testo;

    }
}
