using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RobotChatBubble : MonoBehaviour
{
    public TMP_Text textField; // Riferimento al componente TextMeshPro
    public Color newColor = Color.black; // Il nuovo colore che desideri impostare
    //public string[] stringArray = new string[] { "Benvenuto! Vai al supermercato per comprare gli ingredienti che servono a comporre i panini", "Vai al supermercato per comprare gli ingredienti che servono a comporre i panini", "Vai al fastfood per comporre i panini", "Ci sono dei panini nella cesta, vai dal rider per venderli" };
    
    // Start is called before the first frame update
    void Start()
    {
        if (textField == null)
        {
            Debug.LogError("Il riferimento al componente TextMeshPro non è stato assegnato.");
            return;
        }

        // Imposta il colore del testo al valore specificato
        textField.color = newColor;

        //textField.SetText(stringArray[0]);
        
        //textField.SetText("Benvenuto!\nVai al supermercato\nper comprare gli ingredienti\nche servono a comporre i panini");
        //textField.SetText("Vai al supermercato\nper comprare gli ingredienti\nche servono a comporre i panini");
        //textField.SetText("Vai al fastfood per comporre i panini");
        //textField.SetText("Ci sono dei panini nella cesta,\nvai dal rider per venderli");
    }

    // Update is called once per frame
    void Update()
    {
        //se ha soldi e non ha ne ingredienti ne ingredienti da vendere, va al supermercato
        if (FindObjectOfType<Player>().GetComponent<Player>().Money>0
            && GameManager.Instance.ListaStringhe.Count==0 
            && GameManager.Instance.ListaStringheLeft.Count==0)
        {
            textField.SetText("Leggi il menu del fastfood\nper capire quali ingredienti\nservono per comporre i panini\ne vai al supermarket per comprarli");
        }
        //se ha ingredienti da vendere, va dal rider
        else if (GameManager.Instance.ListaStringheLeft.Count>0)
        {
            textField.SetText("Hai dei panini invenduti,\nvai dal rider per venderli\n last minute");
        }
        //se ha soldi e non ha ingredienti da vendere, va al fast food
        else if (FindObjectOfType<Player>().GetComponent<Player>().Money>0
                 && GameManager.Instance.ListaStringheLeft.Count==0)
        {
            textField.SetText("Vai al ristorante per iniziare \nil turno di lavoro preparando\n gustosi panini con gli ingredienti\n comprati");
        }
    }
}
