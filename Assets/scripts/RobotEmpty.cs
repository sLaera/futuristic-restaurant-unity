using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RobotEmpty : MonoBehaviour
{
    public Transform player; // Riferimento al transform del giocatore
    public float distance; // Distanza davanti al giocatore
    public float offset; // Spostamento laterale rispetto al giocatore, se lo metto negativo va verso destra
    public Vector3 finalPosition;
    public bool start_voice = true;
    // Start is called before the first frame update
    void Start()
    {
        start_voice = true;

        Move();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (player != null)
        {
            // Calcola la posizione davanti al giocatore
            //Vector3 newPosition = player.position + player.forward * distance;
            //transform.position = newPosition;
            //player = FindObjectOfType<Player>().transform;
            Vector3 forwardPosition = player.position + player.forward * distance;

            // Calcola la posizione leggermente spostata verso sinistra
            finalPosition = forwardPosition - player.right * offset;
            
            // Imposta la posizione del GameObject attuale
            transform.position = finalPosition;

        }
    }
    
}
