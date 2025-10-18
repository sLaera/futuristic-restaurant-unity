using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotAudioScript : MonoBehaviour
{
    public AudioSource audioSource1; //audio che dice di andare al supermarket a comprare gli ingredienti
    public AudioSource audioSource2; //audio che dice di andare dal rider
    public AudioSource audioSource3; //audio che dice di andare a fare i panini al fastfood
    public NavMeshAgent robot;
    public Transform robotEmpty;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    public void update_voice()
    {
        if (GameManager.Instance.oldMoney>0
            && GameManager.Instance.ListaStringhe.Count==0 
            && GameManager.Instance.ListaStringheLeft.Count==0)
        {
            //se ha soldi e non ha ne ingredienti ne ingredienti da vendere, va al supermercato
            Debug.Log("vai al super");
            audioSource1.Play();
        }
        //se ha soldi e non ha ingredienti da vendere, va al fast food
        else if (FindObjectOfType<Player>().GetComponent<Player>().Money>0
                 && GameManager.Instance.ListaStringheLeft.Count==0 && GameManager.Instance.ListaStringhe.Count!=0)
        { 
            //se ha soldi e non ha ingredienti da vendere, va al fast food
            Debug.Log("vai al fast");
            audioSource3.Play();
        }else if (GameManager.Instance.ListaStringheLeft.Count>0 )
        {
            Debug.Log("vendi");
            //se ha ingredienti da vendere, va dal rider
            audioSource2.Play();
        }
    }
}
