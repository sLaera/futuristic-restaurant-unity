using System.Collections;
using System.Collections.Generic;
using BurgerDomain;
using UnityEngine;

public class cesta_vendita : MonoBehaviour
{
    // Start is called before the first frame update
    public List<IngredientInfo> WrongBurgers { get; set; }
    void Start()
    {
        
    }

    void svuota()
    {
        WrongBurgers.Clear();
    }
    
    float calcola_totale()
    {
        float totale = 0;
        for (int i = 0; i < WrongBurgers.Count; i++)
        {
            totale = totale + WrongBurgers[i].Prize;
        }
        return totale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
