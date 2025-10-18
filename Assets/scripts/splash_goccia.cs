using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splash_goccia : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject splash;

    private void OnCollisionEnter(Collision other)
    {
        Vector3 posizioneCorrente = transform.position;

        // Rotazione dell'oggetto corrente
        Quaternion rotazioneCorrente = transform.rotation;
        posizioneCorrente = new Vector3(posizioneCorrente.x, posizioneCorrente.y-0.18f, posizioneCorrente.z);
        // Istanzia il prefab nella stessa posizione e con la stessa rotazione
        GameObject nuovoOggetto = Instantiate(splash, posizioneCorrente, rotazioneCorrente);
        nuovoOggetto.transform.Rotate(0,0,180);
        // Elimina l'oggetto corrente
        Destroy(gameObject);
    }
}
