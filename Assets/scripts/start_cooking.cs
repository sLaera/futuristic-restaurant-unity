using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BurgerDomain;
using DG.Tweening;
using UnityEngine;

public class StartCooking : MonoBehaviour
{
    private FirstPersonController _fpsController;
    // Start is called before the first frame update
    void Start()
    {
        _fpsController = FindObjectOfType<FirstPersonController>();
    }
    void OnTriggerEnter(Collider other)
    {
        _fpsController.is_cooking = true;
        _fpsController.transform.DOMove(new Vector3(transform.position.x, _fpsController.transform.position.y, transform.position.z), 1);
        _fpsController.transform.DORotateQuaternion(Quaternion.Euler(0f, -90f, 0f), 1);
        _fpsController.playerCanMove = false;
        _fpsController.enableJump = false;
        _fpsController.enableCrouch = false;
        _fpsController.enableHeadBob = false;

        //----mostro o nascondo gli ingredienti dal tavolo in base a se l'utente li ha o meno
        foreach (var ingredient in FindObjectsOfType<PhysicsGrabbable>(true))
        {
            //se l'utente ha l'ingrediente lo mostro altrimenti lo nascondo
            if (ingredient.CompareTag("ingrediente"))
            {
                if (ingredient.IsSpawner &&
                    FindObjectOfType<Player>().CurrentOwnedIngredients.Any(i => i.GetComponent<IngredientInfo>().Type == ingredient.GetComponent<IngredientInfo>().Type && i.GetComponent<IngredientInfo>().Quantity > 0))
                {
                    ingredient.gameObject.SetActive(true);
                }
                else
                {
                    ingredient.gameObject.SetActive(false);
                }
            }
        }

        GameManager.Instance.StartOrderGeneration();
    }

    //L'uscita è fatta dal game manager

    // private void OnTriggerExit(Collider other)
    // {
    //     GameManager.Instance.StopOrderGeneration();
    //     
    // }

    void Update()
    {

    }
    public void update_table()
    {
        foreach (var ingredient in FindObjectsOfType<PhysicsGrabbable>(true))
        {
            //se l'utente ha l'ingrediente lo mostro altrimenti lo nascondo
            if (ingredient.CompareTag("ingrediente"))
            {
                if (ingredient.IsSpawner &&
                    FindObjectOfType<Player>().CurrentOwnedIngredients.Any(i => i.GetComponent<IngredientInfo>().Type == ingredient.GetComponent<IngredientInfo>().Type && i.GetComponent<IngredientInfo>().Quantity > 0))
                {
                    ingredient.gameObject.SetActive(true);
                }
                else
                {
                    ingredient.gameObject.SetActive(false);
                }
            }
        }
    }
}
