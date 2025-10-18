using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ketchup : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefabgoccia;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetComponent<PhysicsGrabbable>().Isgrabbed==true) // Controlla se è stato effettuato un clic sinistro del mouse
        {
            Vector3 newpos=transform.GetChild(0).transform.position;
            Quaternion rotazioneCorrente = transform.GetChild(0).transform.rotation;
            Instantiate(prefabgoccia, newpos,rotazioneCorrente);
        }
    }
}
