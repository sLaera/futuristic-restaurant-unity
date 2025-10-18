using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour
{
    public int index = 0;
    // Start is called before the first frame update
    public AudioSource audioSource;

    void Start()
    {
        // Assicurati di avere l'AudioSource sull'oggetto
        audioSource = GetComponent<AudioSource>();
    }

    public void Skip()
    {
        
        int cnt=GameObject.FindGameObjectWithTag("cassa_supermercato").GetComponent<CassaSupermercato>().oggetti_da_comprare.Count;
        if (cnt>=1)
        {
            index++;
            if (cnt == index)
            {
                index = 0;
            }

            
        }else
        {
            index = 0;
        }
        audioSource.Play();
        GameObject.FindGameObjectWithTag("testo_qta").GetComponent<show_qta>().Update_text();
    }


}
