using System.Collections.Generic;
using BurgerDomain.Classes;
using UnityEngine;

namespace BurgerDomain
{
    public class MenuBurgerItem : MonoBehaviour
    {
        public string Name;
        public List<IngredientInfo> Ingredients;

        public Order GenerateOrderFromMenuItem(float TimeToLiveOrder)
        {
            var burger = new Burger();
            foreach (var ingredient in Ingredients)
            {
                burger.ingredients.Add(ingredient);
            }
            return new Order(burger, TimeToLiveOrder, Name);
        }
    }
}