using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantError : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Invoke(nameof(HidePanel), 3);
    }

    void HidePanel()
    {
        gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
