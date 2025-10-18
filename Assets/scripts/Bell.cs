using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{ 
    private AudioSource audioSource;

    // Assegna l'AudioSource all'inizio
    void Start()
    {
        // Assicurati di avere l'AudioSource sull'oggetto
        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
        {
            Debug.LogError("L'oggetto non ha un componente AudioSource.");
        }
    }

    // Chiamato quando vuoi far suonare la canzone
    public void SuonaCanzone()
    {
        // Assicurati che l'AudioSource abbia un AudioClip assegnato
        if (audioSource != null && audioSource.clip != null)
        {
            // Riproduci l'AudioClip
            audioSource.Play();
            GameObject.FindGameObjectWithTag("mieingred").GetComponent<riepilogo_ingredienti_in_possesso>().Riepilogo();

        }
        else
        {
            Debug.LogError("AudioSource o AudioClip non assegnato correttamente.");
        }
    }
}

