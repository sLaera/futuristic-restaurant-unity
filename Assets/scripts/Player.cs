using System;
using System.Collections.Generic;
using BurgerDomain;
using BurgerDomain.Classes;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    //[FormerlySerializedAs("currentOwnedIngredients")]
    public List<IngredientInfo> CurrentOwnedIngredients = new List<IngredientInfo>();
    [FormerlySerializedAs("ingredientiDaPagare")]
    public List<IngredientInfo> IngredientiDaPagare = new List<IngredientInfo>();
    public List<IngredientInfo> LeftoverIngredients  = new List<IngredientInfo>();
    public float Money { get; set; }
    public Player oldPlayerData;
    public Vector3 old_pos;

    void Start()
    {
        CurrentOwnedIngredients = new List<IngredientInfo>();
        LeftoverIngredients = new List<IngredientInfo>();
        Money = 100f;

        Money=GameManager.Instance.oldMoney;
        ConvertList();
        ConvertListLeft();

        if (GameManager.Instance.oldMoney > 0)
        {
            Money=GameManager.Instance.oldMoney;
        }
        
         //test
        // foreach (var i in GameManager.Instance.AllPossibleIngredients)
        // {
        //     i.Quantity = 10;
        //     CurrentOwnedIngredients.Add(i);
        // }
        

        if (GameManager.Instance.intosupermarket == false && GameManager.Instance.intorestaurant ==false )
        {
            if (GameManager.Instance.cameraforw == 1)
            {
                // per supermercato 
                transform.position = new Vector3(10.84f,2f,12.69f);
                transform.Rotate(Vector3.up * 180f);
            }
            if (GameManager.Instance.cameraforw == 2)
            {
                // per risto 
                
                transform.position = new Vector3(-4.543105f,2.00484f,-47.8713f);
                transform.Rotate(Vector3.up * -45f);
            }
        }
        FindObjectOfType<loadingpanel>().gameObject.SetActive(false);
    }
    public void ConvertList()
    {
        List<IngredientInfo> allingredients=GameManager.Instance.AllPossibleIngredients;
        List<IngredientType> types =GameManager.Instance.ListaStringhe;
        List<float> prices  =GameManager.Instance.Listaprezzi;
        List<int> qtas =GameManager.Instance.Listaqta;
        for (int i = 0; i < types.Count; i++)
        {
            for (int j = 0; j < allingredients.Count; j++)
            {
                if (allingredients[j].Type ==
                    types[i])
                {
                    IngredientInfo copia;
                    copia = allingredients[j];
                    copia.uid=Guid.NewGuid();
                    copia.Quantity=qtas[i];
                    copia.Prize=prices[i];
                    CurrentOwnedIngredients.Add(copia);
                    //copia.SetActive(false);
                }
            }

        }

    }
    public void ConvertListLeft()
    {
        List<IngredientInfo> allingredients=GameManager.Instance.AllPossibleIngredients;
        List<IngredientType> types =GameManager.Instance.ListaStringheLeft;
        List<float> prices  =GameManager.Instance.ListaprezziLeft;
        List<int> qtas =GameManager.Instance.ListaqtaLeft;
        for (int i = 0; i < types.Count; i++)
        {
            for (int j = 0; j < allingredients.Count; j++)
            {
                if (allingredients[j].GetComponent<IngredientInfo>().Type ==
                    types[i])
                {
                    IngredientInfo copia;
                    copia = allingredients[j];
                    copia.uid=Guid.NewGuid();
                    copia.Quantity=qtas[i];
                    copia.Prize=prices[i];
                    LeftoverIngredients.Add(copia);
                    //copia.SetActive(false);
                }
            }

        }

    }

    public void AddMoney(float delta)
    {
        Money += delta;
    }

    public void SubMoney(float delta)
    {
        Money -= delta;
        if (Money<=0)
        {
            Money = 0;
        }
    }

    //usato al cambio scena per salvare i dati
    public void CloneData(Player player)
    {
        if (CurrentOwnedIngredients.Count > 0)
        {
            CurrentOwnedIngredients = player.CurrentOwnedIngredients;
        }

        if (LeftoverIngredients.Count > 0)
        {
            LeftoverIngredients = player.LeftoverIngredients;
        }
        
        Money = player.Money;
    }
}
