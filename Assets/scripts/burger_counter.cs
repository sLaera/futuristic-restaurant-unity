using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class burger_counter : MonoBehaviour
{
    // Start is called before the first frame update
    public int limit;

    // Update is called once per frame
    private void Start()
    {
        GetComponent<Image>().enabled = false;
    }

    void Update()
    {
        if (GameManager.Instance.n_ordini_corretti > limit)
        {
            GetComponent<Image>().enabled = true;
        }
        else
        {
            GetComponent<Image>().enabled = false;
        }

    }
}
