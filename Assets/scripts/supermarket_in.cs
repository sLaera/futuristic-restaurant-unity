using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class supermarket_in : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<FPSInteractionManager>().intosupermarket = true;
            GameManager.Instance.intosupermarket = true;
        }
        GameObject.FindGameObjectWithTag("music").GetComponent<BackgroundMusic>().PlayBackgroundMusic();
        GameObject.FindGameObjectWithTag("music_background").GetComponent<BackgroundMusic>().PlayBackgroundMusic();
    }
}
