using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BurgerDomain;
using UnityEngine;

public class LeftoverChest : MonoBehaviour
{
    public GameObject PlatePosition;
    public GameObject PlatePrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("piatto") && !other.GetComponent<PhysicsGrabbable>().Isgrabbed)
        {
            var listIngredient = other.GetComponent<Creazionepanino>()?.GetBurger()?.ingredients;
            if (listIngredient != null)
            {
                GameManager.Instance.Player.LeftoverIngredients.AddRange(listIngredient);
                GameManager.Instance.ConvertListLeft(listIngredient);
                // ho aggiunto gli ingredienti del panino al quelli
                // che posso vendere last minute
                
                // ora devo aggiornare i miei ingredienti 
                bool var = false;
                for (int i = 0; i < listIngredient.Count; i++)
                {
                    var = false;
                    for (int j = 0; j < GameManager.Instance.ListaStringhe.Count && var==false; j++)
                    {
                        if (listIngredient[i].GetComponent<IngredientInfo>().Type ==
                            GameManager.Instance.ListaStringheLeft[j])
                        {
                            if ((GameManager.Instance.ListaqtaLeft[j] - listIngredient[i].GetComponent<IngredientInfo>().Quantity) == 0)
                            {
                                GameManager.Instance.ListaStringhe.RemoveAt(j);
                                GameManager.Instance.Listaqta.RemoveAt(j);
                                GameManager.Instance.Listaprezzi.RemoveAt(j);
                                var = true;
                            }
                            else
                            {
                                GameManager.Instance.ListaqtaLeft[j] -= listIngredient[i].GetComponent<IngredientInfo>().Quantity;
                                var = true;
                            }
                        }
                    }
                }

                other.GetComponent<Creazionepanino>().ListaIngredienti.Clear();
            }
            
            // Iterate through all child objects
            foreach (Transform child in other.transform)
            {
                // Deactivate each child object
                child.gameObject.SetActive(false);
            }
        }
    }
}
