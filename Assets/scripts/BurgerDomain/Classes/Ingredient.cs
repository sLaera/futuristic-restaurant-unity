using UnityEngine;

namespace BurgerDomain.Classes
{
    /// <summary>
    /// Classe Ausiliaria utilizzata per il flusso di creazione degli ordini, ogni Ingrediente è associato a un ingrediente di
    /// GameManager.AllIngredeints e può essere associato a un oggetto correntemente in gioco
    /// </summary>
    public class Ingredient
    {
        public IngredientInfo IngredientInfo { set; get; }
        public GameObject GameObject { get; set; }
        
        public Ingredient(IngredientInfo ingredientInfo)
        {
            GameObject = null;
            IngredientInfo = ingredientInfo;
        }

        /// <summary>
        /// Se esiste vuol dire che esiste un game object collegato ad esso
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            return GameObject != null;
        }
    }
}
