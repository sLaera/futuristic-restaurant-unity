using System;
using BurgerDomain.Classes;
using UnityEngine;

namespace BurgerDomain
{
    public class IngredientInfo : MonoBehaviour
    {
        public IngredientType Type;
        public float Prize;
        public int Quantity;
        public Guid uid=Guid.NewGuid();

        public IngredientInfo(IngredientType type, float prize, int quantity)
        {
            Type = type;
            Prize = prize;
            Quantity = quantity;
        }
    }

}
