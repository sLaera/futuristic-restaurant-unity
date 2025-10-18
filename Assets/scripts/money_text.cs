using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class money_text : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI testomoney;

    // Update is called once per frame
    private void Start()
    {
        testomoney = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        float m = FindObjectOfType<Player>().Money; 
        testomoney.text = "Money: " + m;
    }
}
