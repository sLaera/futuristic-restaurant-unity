using System.Collections;
using System.Collections.Generic;
using BurgerDomain;
using UnityEngine;

public class AddButton : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    void Start()
    {
        // Assicurati di avere l'AudioSource sull'oggetto
        audioSource = GetComponent<AudioSource>();
    }
    public void Add()
    {
        int cnt=GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>().oggetti_da_comprare.Count;
        if (cnt > 0)
        {
            int index = GameObject.FindGameObjectWithTag("skip_button").GetComponent<SkipButton>().index;
            Debug.Log("index " + index);
            GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>()
                .oggetti_da_comprare[index].GetComponent<IngredientInfo>().Quantity += 1;
            audioSource.Play();
            GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>()
                .AggiornaTotaleDaPagare();
            GameObject.FindGameObjectWithTag("testo_qta").GetComponent<show_qta>().Update_text();
        }
    }

    // Update is called once per frame

}
