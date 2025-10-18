using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public List<AudioClip> backgroundMusic;
    public AudioSource audioSource;

    void Start()
    {
        // Assicurati di avere un AudioSource componente collegato allo stesso GameObject
        audioSource = GetComponent<AudioSource>();

        // Assegna l'AudioClip alla sorgente audio


        audioSource.clip = backgroundMusic[2];

        // Riproduci la musica di sottofondo in loop
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayBackgroundMusic()
    {
        if( GameManager.Instance.intosupermarket==true)
        {
            audioSource.clip = backgroundMusic[0];

            // Riproduci la musica di sottofondo in loop
            audioSource.loop = true;
            audioSource.Play();
        }
        if (GameManager.Instance.intorestaurant==true)
        {
            audioSource.clip = backgroundMusic[1];

            // Riproduci la musica di sottofondo in loop
            audioSource.loop = true;
            audioSource.Play();
        }

        if (GameManager.Instance.intorestaurant==false && GameManager.Instance.intosupermarket == false)
        {
            audioSource.clip = backgroundMusic[2];

            // Riproduci la musica di sottofondo in loop
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
