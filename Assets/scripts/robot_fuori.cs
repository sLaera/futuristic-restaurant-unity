using System;
using System.Collections;
using System.Collections.Generic;
using BurgerDomain;
using BurgerDomain.Classes;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
using Unity.VisualScripting;



public class Robotfuori : MonoBehaviour
{

    //Set these Textures in the Inspector
    public Texture HappyScreen;
    public Material ScreenMaterial;
    // Start is called before the first frame update
    void Start()
    {

        ScreenMaterial.mainTexture = HappyScreen;
 
    }

    private void Update()
    {
        ScreenMaterial.mainTexture = HappyScreen;
    }
}
