using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activate_lights : MonoBehaviour
{
    private GameObject oggettoConTagNight;
    private bool flag_activate =true;
    public float nuovaIntensita = 30.0f;
    void Start()
    {

            // Ottieni il materiale dal renderer
            oggettoConTagNight = GameObject.FindGameObjectWithTag("light");
            textureSkySwap scriptAssociato = oggettoConTagNight.GetComponent<textureSkySwap>();

            // Verifica che lo script sia stato trovato
            if (scriptAssociato != null)
            {
                // Chiamare il metodo pubblico
                bool flag=scriptAssociato.Is_night();
            
                // Controlla se il materiale Skybox ha il tag "night"
                if (flag==true && flag_activate==false)
                {
                    // Se il tag è "night", rendi il materiale emissivo
                     // Puoi impostare il colore dell'emissione come preferisci
                    GameObject oggettoDaDisabilitare = this.gameObject;

                    // Disabilita l'oggetto
                    oggettoDaDisabilitare.SetActive(true);
                    flag_activate=true;
                }
                if(flag==false){

                    GameObject oggettoDaDisabilitare = this.gameObject;

                    // Disabilita l'oggetto
                    oggettoDaDisabilitare.SetActive(false);
                }
            }
        
    }

    // Funzione per impostare l'emissione su un materiale

    void Update(){

            oggettoConTagNight = GameObject.FindGameObjectWithTag("light");
            textureSkySwap scriptAssociato = oggettoConTagNight.GetComponent<textureSkySwap>();

            // Verifica che lo script sia stato trovato
            if (scriptAssociato != null)
            {
                // Chiamare il metodo pubblico
                bool flag=scriptAssociato.Is_night();
            
                // Controlla se il materiale Skybox ha il tag "night"
                if (flag==true && flag_activate==false)
                {
                    
                    // Puoi impostare il colore dell'emissione come preferisci
                    GameObject oggettoDaDisabilitare = this.gameObject;
                   

                    // abilita l'oggetto
                    oggettoDaDisabilitare.SetActive(true);
                }
                if(flag==false){
                    GameObject oggettoDaDisabilitare = this.gameObject;

                    // Disabilita l'oggetto
                    oggettoDaDisabilitare.SetActive(false);
                }
            }
        
    }
}
