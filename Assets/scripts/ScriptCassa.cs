using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptCassa : MonoBehaviour
{
    public TextMeshPro Testo;
    public float Saldo = 50.0f;
    
    void onTriggerEnter()
    {
        //viene chiamata quando l'utente entra nel box collider
    }
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText()
    {
        if (Testo == null)
        {
            Debug.LogError("Assicurati di assegnare l'oggetto TextMeshPro nell'Inspector.");
            return;
        }
        
        string displayText = string.Format("Money: {0:C}\nSaldo: {1:C}", FindObjectOfType<Player>().Money, Saldo);
        Testo.text = displayText;
        
        
    }
    
    public void AcquistaCarrello() 
    { 
        GetComponent<Player>().SubMoney(Saldo);
        
        UpdateText();
    }
            

}
