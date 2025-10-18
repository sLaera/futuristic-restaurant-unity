using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check_ingredients : MonoBehaviour
{
    void Start()
    {
        // if (Check())
        // {
        //     this.gameObject.SetActive(true);
        // }
        // else
        // {
        //     this.gameObject.SetActive(false);
        // }
    }

    // Start is called before the first frame update
    public bool  Check()
    {
        if (GameManager.Instance.ListaStringhe.Count> 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
