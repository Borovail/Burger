using DefaultNamespace;
using Item;
using UnityEngine;

namespace KitchenTools
{
    public class DishMaker : MonoBehaviour
    {
        [SerializeField] private Receipt receipt;
        [SerializeField] private Transform ingridientPlace;
        [SerializeField] private Dish dishPrefab;

        private Dish dish;

        
        //DEBUG
        public void Awake()
        {
            dish = Instantiate(dishPrefab);
            dish.Setup(ingridientPlace, receipt);
        }
        //
        public bool HasCookedIngridient => !dish || receipt.Ingredients.Count <= dish.Ingredients.Count;

        public bool CanGetIngridient(Ingredient ingredient)
        {
            return !HasCookedIngridient;
        }

        private void ReceiveIngridient(Ingredient ingredient)
        {
            if (!dish)
            {
                dish = Instantiate(dishPrefab);
                dish.OnPickedUp += OnDishPickedUp;
                dish.Setup(ingridientPlace, receipt);
            }
            dish.AddIngridient(ingredient);
        }

        private void OnDishPickedUp()
        {
            dish.OnPickedUp -= OnDishPickedUp;
        }

        private void RemoveIngridient(Ingredient ingredient)
        {
            if (!dish) return;
            dish.RemoveIngridient(ingredient);
        }

        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Ingridient))
            {
                Ingredient ingredient = other.GetComponent<Ingredient>();
                if (CanGetIngridient(ingredient))
                {
                    ReceiveIngridient(ingredient);
                }
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Tags.Ingridient))
            {
                RemoveIngridient(other.GetComponent<Ingredient>());
            }
        }
    }
}