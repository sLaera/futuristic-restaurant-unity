using System.Collections;
using System.Collections.Generic;
using BurgerDomain;
using Unity.VisualScripting;
using UnityEngine;

public class ingredientidacomprare : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IngredientInfo>() && !GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>().oggetti_da_comprare.Contains(other.gameObject))
        {
            // potrei metterli direttamente nella cassa senza passare dal player
            //GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>().IngredientiDaPagare.Add(collision.gameObject)
            GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>().oggetti_da_comprare
                .Add(other.gameObject);
            //other.gameObject.GetComponent<Collider>().enabled = false;
            GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>()
                .AggiornaTotaleDaPagare();
            GameObject.FindGameObjectWithTag("testo_qta").GetComponent<show_qta>().Update_text();
            // considero di settare a false gli ingredienti nella cassa da comprare 
        }
    }

    

}
