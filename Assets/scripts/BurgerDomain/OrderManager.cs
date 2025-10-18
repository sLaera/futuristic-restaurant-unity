using System;
using System.Collections.Generic;
using System.Linq;
using BurgerDomain.Classes;
using Unity.VisualScripting;
using UnityEngine;
using Burger = BurgerDomain.Classes.Burger;
using Random = UnityEngine.Random;

namespace BurgerDomain
{
    public class OrderManager
    {

        public static Order GenerateNewOrder(List<MenuBurgerItem> menu, float TimeToLiveOrder)
        {
            var index = Random.Range(0, menu.Count);
            var menuItem = menu[index];
            //conversione da elemento del mnu a un burger
            return menuItem.GenerateOrderFromMenuItem(TimeToLiveOrder);
        }


        /*
        static GameObject SelectElementWithProbability(Dictionary<GameObject, float> elementList)
        {
            double eventProbability = Random.value;
            Console.WriteLine("Extraction Debug : pEvent = " + eventProbability);

            double lowerProbability = 0d;
            GameObject candidate = elementList.Last().Key;
            foreach (var item in elementList)
            {
                if (lowerProbability <= eventProbability && eventProbability < (lowerProbability + item.Value))
                {
                    candidate = item.Key;
                    break;
                }
                lowerProbability += item.Value;
            }
            return candidate;
        }

        // GameObject GetObjectWithMaxProb()
        // {
        //     int totalWeight = objectList.Sum(t => t.priority); // Using LINQ for suming up all the values
        //     int randomNumber = rnd.Next(0, totalWeight);
        //
        //     GameObject myGameObject = null;
        //     foreach (RandomObject item in objectList)
        //     {
        //         if(randomNumber < item.priority)
        //         {
        //             myGameObject = item.randomObj;
        //             break;
        //         }
        //         randomNumber -= item.priority;
        //     }
        //     return myGameObject;
        // }

        private static float GetProbability(int quantity, int quantityTot, int numberOfPossibleIngredients, float probabilityOfRunOutIngredients)
        {
            return (quantity + probabilityOfRunOutIngredients) / (quantityTot + (probabilityOfRunOutIngredients * numberOfPossibleIngredients));
        }

        private static Dictionary<GameObject, float> ProbabilityMap(List<GameObject> currentOwnedIngredients, float probabilityOfRunOutIngredients, Dictionary<IngredientType, int>quantityTotForType)
        {

            var probabilityMap = new Dictionary<GameObject, float>();
            var quantityTot = currentOwnedIngredients
                .Aggregate(0, (totQty, ingredient) => totQty + ingredient.GetComponent<IngredientInfo>().Quantity);
            var numberOfPossibleIngredients = GameManager.Instance.AllPossibleIngredients.Count;
            foreach (var gameObject in GameManager.Instance.AllPossibleIngredients)
            {
                var probability = GetProbability(quantityTotForType[gameObject.GetComponent<IngredientInfo>().Type], quantityTot, numberOfPossibleIngredients, probabilityOfRunOutIngredients);
                probabilityMap.Add(gameObject, probability);
            }
            return probabilityMap;
        }

        public static Order GenerateNewOrderOLD(List<GameObject> currentOwnedIngredients, int numberOfIngredientsInBurger, float probabilityOfRunOutIngredients, float timeToLiveOrder)
        {
            var quantityTotForType = new Dictionary<IngredientType, int>();
            foreach (var gameObject in GameManager.Instance.AllPossibleIngredients)
            {
                var quantityTot = currentOwnedIngredients
                    .Aggregate(0, (totQty, i) => totQty + i.GetComponent<IngredientInfo>().Type == gameObject.GetComponent<IngredientInfo>().Type ? i.GetComponent<IngredientInfo>().Quantity : 0);
                quantityTotForType[gameObject.GetComponent<IngredientInfo>().Type] = quantityTot;
            }

            var probabilityMap = ProbabilityMap(currentOwnedIngredients, probabilityOfRunOutIngredients, quantityTotForType);

            var burger = new Burger();

            for (int i = 0; i < numberOfIngredientsInBurger; i++)
            {
                var isIngredientFound = false;
                //seleziona un ingrediente
                var ingredientInfo = probabilityMap.Keys.First();
                //for di 100 elementi per non ciclare all'infinito
                for (int j = 0; j < 100 || !isIngredientFound; j++)
                {
                    ingredientInfo = SelectElementWithProbability(probabilityMap);
                    //un ingrediente deve essere selezionato non più di due volte. Passo al prossimo ciclo per sperare di trovare un nuovo ingrediente
                    if (burger.ingredients.FindAll(ing => ing.GetComponent<IngredientInfo>().Type == ingredientInfo.GetComponent<IngredientInfo>().Type).Count > 2)
                    {
                        probabilityMap.Remove(ingredientInfo);// questo elemento non potrà più essere selezionato
                        continue;
                    }
                    isIngredientFound = true;
                }
                burger.ingredients.Add(ingredientInfo);

                //aggiornamento della mappa
                quantityTotForType[ingredientInfo.GetComponent<IngredientInfo>().Type] -= 1;
                probabilityMap = ProbabilityMap(currentOwnedIngredients, probabilityOfRunOutIngredients, quantityTotForType);
            }
            return new Order(burger, timeToLiveOrder);
        }*/
    }
}
