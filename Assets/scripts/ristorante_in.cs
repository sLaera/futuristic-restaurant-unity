using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ristorante_in : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        if (GameManager.Instance.ListaStringhe.Count > 0)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<FPSInteractionManager>().intorestaurant = true;
            GameManager.Instance.intorestaurant = true;
        }
        GameObject.FindGameObjectWithTag("music").GetComponent<BackgroundMusic>().PlayBackgroundMusic();
        GameObject.FindGameObjectWithTag("music_background").GetComponent<BackgroundMusic>().PlayBackgroundMusic();
    }
}
