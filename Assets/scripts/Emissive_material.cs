using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emissive_material : MonoBehaviour
{
    // Start is called before the first frame update    public Material skyboxMaterial; // Trascina il materiale Skybox nell'ispettore
    private new Renderer _renderer;
    public Color mat;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        mat=GetComponent<Renderer>().material.color;
        // Verifica che il renderer non sia nullo
        if (_renderer != null)
        {
            // Ottieni il materiale dal renderer
            Material materialeAssociato = _renderer.material;
            DateTime orarioCorrente = DateTime.Now;
            bool oggettoConTagNight = GameObject.FindGameObjectWithTag("light").GetComponent<textureSkySwap>().Is_night();
            if ((orarioCorrente.Hour >= 17 && orarioCorrente.Hour <= 24 )|| (orarioCorrente.Hour >= 0 && orarioCorrente.Hour <= 6)){
                oggettoConTagNight=true;
            }
            // Verifica che lo script sia stato trovato
            if (oggettoConTagNight)
            { 
                SetEmission(materialeAssociato,mat); // Puoi impostare il colore dell'emissione come preferisci
            }
        }
    }

    // Funzione per impostare l'emissione su un materiale
    void SetEmission(Material material, Color emissionColor)
    {
        material.EnableKeyword("_EMISSION");
        material.SetColor(EmissionColor, emissionColor);
    }

    
}
