using System;
using System.Collections.Generic;
using BurgerDomain;
using BurgerDomain.Classes;
using UnityEngine;
using TMPro;

public class GestioneTesto : MonoBehaviour
{
    public TextMeshPro testoMeshPro;
    public int counter;
    public IngredientType type;
    public int old_cnt = 0;
    void Start()
    {
        // Assicurati che l'oggetto TextMeshPro sia assegnato nell'Inspector di Unity
        if (testoMeshPro == null)
        {
            Debug.LogError("Assicurati di assegnare l'oggetto TextMeshPro nell'Inspector.");
            return;
        }
    }

    void AggiornaTesto()
    {
        var numberOfIngredientLeft = FindObjectOfType<Player>().CurrentOwnedIngredients
            .Find(i => i.GetComponent<IngredientInfo>().Type == type)?
            .GetComponent<IngredientInfo>().Quantity;
        old_cnt = counter;
        if (numberOfIngredientLeft != null)
            counter = (int)numberOfIngredientLeft;
        else
            counter = 0;

        if (counter <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        
        if (counter != old_cnt)
        {
            testoMeshPro.text = counter.ToString();
        }
    }

    private void Update()
    {
        AggiornaTesto();
    }


    public void ModificaVariabile()
    {
        AggiornaTesto();
    }
}

