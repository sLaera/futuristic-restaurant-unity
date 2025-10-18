using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BurgerDomain;
using BurgerDomain.Classes;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public List<string> ScenesToLoad;
    public List<string> ScenesToUnload;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.intosupermarket == true)
            {
                GameManager.Instance.cameraforw = 1;
            }
            if (GameManager.Instance.intorestaurant == true)
            {
                GameManager.Instance.cameraforw = 2;
            }
            if (GameManager.Instance.intosupermarket == false && GameManager.Instance.intorestaurant == false)
            {
                GameManager.Instance.old_pos = FindObjectOfType<Player>().transform.position;
                if (GameManager.Instance.intosupermarket == false && GameManager.Instance.intorestaurant == false)
                    
                {
                    GameManager.Instance.cameraforw = 0;
                }

            }

            //Se non ho ingredienti non posso entrare nel ristorante
            if (ScenesToLoad.Any(s => s == "Fast Food Scene table area") && FindObjectOfType<Player>().CurrentOwnedIngredients.Count == 0 )
            {
                FindObjectOfType<RestaurantError>(true).gameObject.SetActive(true);
                return;
            }

            if (GameManager.Instance.ListaStringhe.Count > 0 || FindObjectOfType<Player>().GetComponent<Player>().Money>0)
            {
                GameManager.Instance.LoadUnloadScenes(ScenesToLoad, ScenesToUnload);
            }
            if (GameManager.Instance.ListaStringhe.Count == 0 && FindObjectOfType<Player>().GetComponent<Player>().Money==0)
            {
                GameManager.Instance.GetComponent<end_game>().Check();
            }
        }
    }
}
