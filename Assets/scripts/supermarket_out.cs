using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class supermarket_out : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<Player>().GetComponent<FPSInteractionManager>().intosupermarket = false;
            GameManager.Instance.intosupermarket = false;
        }
        GameObject.FindGameObjectWithTag("music").GetComponent<BackgroundMusic>().PlayBackgroundMusic();
        GameObject.FindGameObjectWithTag("music_background").GetComponent<BackgroundMusic>().PlayBackgroundMusic();
    }
}
