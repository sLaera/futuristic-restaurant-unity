using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BurgerDomain.Classes
{
    public class Burger
    {
        public List<IngredientInfo> ingredients;
        public Bread topBread;
        public Bread bottomBread;
        public GameObject gameObject;//todo: capire se serve

        public Burger(List<IngredientInfo> ingredients, Bread topBread, Bread bottomBread)
        {
            this.ingredients = ingredients;
            this.topBread = topBread;
            this.bottomBread = bottomBread;
        }
        
        public Burger(Bread topBread, Bread bottomBread)
        {
            this.ingredients = new List<IngredientInfo>();
            this.topBread = topBread;
            this.bottomBread = bottomBread;
        }
        
        /// <summary>
        /// Crea un burger vuoto. Questo per definizione è incompleto. Usato per i burger degli ordini
        /// </summary>
        public Burger()
        {
            this.ingredients = new List<IngredientInfo>();
            this.topBread = null;
            this.bottomBread = null;
        }

        /// <summary>
        /// Controlla se è un panino finito. Ovvero formato da: [PANE-INGREDIENTI-PANE]
        /// </summary>
        /// <returns></returns>
        public bool IsComplete()
        {
            return topBread != null && bottomBread != null && ingredients.Count != 0;
        }

        public float GetTotalCost()
        {
            return ingredients.Aggregate(0f, (sum, i) => sum + i.GetComponent<IngredientInfo>().Prize);
        }

    }
}
