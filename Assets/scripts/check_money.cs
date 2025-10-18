using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check_money : MonoBehaviour
{
    // Start is called before the first frame update
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
        if (GameManager.Instance.oldMoney > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
