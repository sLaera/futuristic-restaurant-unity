using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BurgerDomain;
using BurgerDomain.Classes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Creazionepanino : MonoBehaviour
{
    public List<GameObject> ListaIngredienti;
    private Quaternion _startRotation;

    //la gestione dell'inserimento degli ingredienti è dentro pydsic grabbable
    void Start()
    {
        ListaIngredienti = new List<GameObject>();
        _startRotation = transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = _startRotation;
    }

    public Burger GetBurger()
    {
        var burger = new Burger();
        if (ListaIngredienti.Count > 0)
        {
            burger.bottomBread = ListaIngredienti.First()?.GetComponent<Bread>();
            burger.topBread = ListaIngredienti.Last()?.GetComponent<Bread>();

            for (int i = 1; i < ListaIngredienti.Count - 1; i++)
            {
                burger.ingredients.Add(ListaIngredienti[i].GetComponent<IngredientInfo>());
            }

            return burger;
        }
        else
        {
            return null;
        }
    }
}
