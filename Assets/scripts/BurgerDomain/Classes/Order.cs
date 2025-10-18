namespace BurgerDomain.Classes
{
    public class Order
    {
        public Burger Burger { set; get; }
        public OrderStatus Status { set; get; }
        public float TimeLeft { set; get; }

        public string Name { set; get; }

        public Order(Burger burger, float timeToLive, string name)
        {
            this.Burger = burger;
            TimeLeft = timeToLive;
            this.Status = OrderStatus.Created;
            Name = name;
        }

        public Order()
        {
        }

        /// <summary>
        /// Compara il burger dato con il burger dell'ordine. Se non sono uguali ritorna false altrimenti true
        /// </summary>
        /// <param name="burgerToCompare"></param>
        /// <returns></returns>
        public bool CompareBurgerWithOrder(Burger burgerToCompare)
        {
            //se non è completo allora è sbagliato a prescindere
            if (burgerToCompare.IsComplete())
            {
                int i = 0;
                if (Burger.ingredients.Count == burgerToCompare.ingredients.Count)
                {
                    foreach (var ingredient in Burger.ingredients)
                    {
                        if (ingredient.GetComponent<IngredientInfo>()?.Type !=
                            burgerToCompare.ingredients[i]?.GetComponent<IngredientInfo>()?.Type)
                        {
                            return false;
                        }

                        i++;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
