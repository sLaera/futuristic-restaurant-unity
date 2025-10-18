using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class textureSkySwap : MonoBehaviour
{
    public Material NotteSkybox;
    public Material TramontoSkybox;
    public Material AlbaSkybox;
    public Material GiornoSkybox;
    public bool notte=false;
    public DateTime orarioCorrente = DateTime.Now;


    private int inizioGiorno = 8;
    private int fineGiorno = 17;
    private int fineNotte = 6;
    private int fineTramonto = 18;
    void Start()
    {
        Debug.Log(orarioCorrente.Hour);
        if (orarioCorrente.Hour >= inizioGiorno && orarioCorrente.Hour <= fineGiorno){
            RenderSettings.skybox = GiornoSkybox;
            GetComponent<Light>().intensity = 0.8f;
            Debug.Log("giorno");
        }
        if ((orarioCorrente.Hour >= fineGiorno && orarioCorrente.Hour <= 24 )|| (orarioCorrente.Hour >= 0 && orarioCorrente.Hour <= fineNotte)){
            RenderSettings.skybox = NotteSkybox;
            GetComponent<Light>().intensity = 0.1f;
            notte=true;
            Debug.Log("notte");
        }
        if (orarioCorrente.Hour >= fineNotte && orarioCorrente.Hour <= inizioGiorno){
            RenderSettings.skybox = AlbaSkybox;
            GetComponent<Light>().intensity = 0.2f;
            notte=false;
        }
        if (orarioCorrente.Hour >= fineGiorno && orarioCorrente.Hour <= fineTramonto){
            RenderSettings.skybox = TramontoSkybox;
            GetComponent<Light>().intensity = 0.2f;
        }
        
    }

    public bool Is_night(){
        if ((orarioCorrente.Hour >= fineGiorno && orarioCorrente.Hour <=24) ||  (orarioCorrente.Hour >=0 && orarioCorrente.Hour < inizioGiorno)){
            return true;
        }else{
            return false;
        }
    }
}
