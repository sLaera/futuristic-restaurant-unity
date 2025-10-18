using BurgerDomain;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using BurgerDomain.Classes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;

public class LastMinuteScreen : MonoBehaviour
{
    public IngredientInfo element = null;
    public TextMeshPro TestoLastMinute;
    private bool tuttogiusto;
    RobotCameriereScript scriptRobot;
    public bool pulse = false;
    public bool fine = false;
    //private Player ordini;
    // Start is called before the first frame update
    void Start()
    {
        scriptRobot = GameObject.FindGameObjectWithTag("RobotCameriere").GetComponent<RobotCameriereScript>();
       // ordini = FindObjectsByType<Player>;
       pulse = false;
    }
    // Update is called once per frame
    void Update()
    {
        
        // Controllo se il robot � in posizione, se  non lo � esce questa scritta
        if (scriptRobot._positionNumber == 0 || scriptRobot._positionNumber == 3 || scriptRobot._positionNumber == 4 || scriptRobot._positionNumber == 2)
        {
            TestoLastMinute.text = "SEI IN ATTESA DI UN NUOVO ORDINE!";
        }
        // check ingredients uno per volta e poi se tutto ok variabile tuttogiusto = true else false 
        if(scriptRobot._positionNumber == 1  && scriptRobot._order.TimeLeft >= 0 && fine == false)
        {
            fine = true;
            foreach (var ingredient in scriptRobot._order.Burger.ingredients)
            {
                if (ingredient.Type != IngredientType.Ketchup && ingredient.Type != IngredientType.Mayo)
                {
                    element = ingredient.GetComponent<IngredientInfo>();

                    if (GameManager.Instance.ListaStringhe.Contains(element.Type))
                    {
                        tuttogiusto = true;
                    }
                    else
                    {
                        tuttogiusto = false;
                        break;
                    }
                }
            }
            if (tuttogiusto == false )
            {
                TestoLastMinute.text = "NON POSSIEDI L'INGREDIENTE " + element.Type.ToString().ToUpper() + ", SE VUOI ACQUISTARLO 'LAST MINUTE' AL DOPPIO DEL PREZZO CLICCA SUL PULSANTE";
                pulse = true;
            }
            else if (tuttogiusto == true)
            {
                TestoLastMinute.text = "POSSIEDI TUTTI GLI INGREDIENTI NECESSARI \n PROCEDI ALLA CREAZIONE DELL'ORDINE!";
            }


        }
        
         
         
    }


}
