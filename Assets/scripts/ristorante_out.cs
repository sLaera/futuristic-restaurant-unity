using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ristorante_out : MonoBehaviour
{
    // Start is called before the first frame update
    RobotCameriereScript script;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<Player>().GetComponent<FPSInteractionManager>().intorestaurant = false;
            script = GameObject.FindGameObjectWithTag("RobotCameriere").GetComponent<RobotCameriereScript>();
            script.ScreenMaterial.mainTexture = script.HappyScreen;
            GameManager.Instance.intorestaurant = false;
        }
        GameObject.FindGameObjectWithTag("music").GetComponent<BackgroundMusic>().PlayBackgroundMusic();
        GameObject.FindGameObjectWithTag("music_background").GetComponent<BackgroundMusic>().PlayBackgroundMusic();
    }
}
