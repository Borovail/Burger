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

        public bool CanGetIngridient(Ingridient ingridient)
        {
            return !HasCookedIngridient;
        }

        private void ReceiveIngridient(Ingridient ingridient)
        {
            if (!dish)
            {
                dish = Instantiate(dishPrefab);
                dish.OnPickedUp += OnDishPickedUp;
                dish.Setup(ingridientPlace, receipt);
            }
            dish.AddIngridient(ingridient);
        }

        private void OnDishPickedUp()
        {
            dish.OnPickedUp -= OnDishPickedUp;
        }

        private void RemoveIngridient(Ingridient ingridient)
        {
            if (!dish) return;
            dish.RemoveIngridient(ingridient);
        }

        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Ingridient))
            {
                Ingridient ingridient = other.GetComponent<Ingridient>();
                if (CanGetIngridient(ingridient))
                {
                    ReceiveIngridient(ingridient);
                }
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Tags.Ingridient))
            {
                RemoveIngridient(other.GetComponent<Ingridient>());
            }
        }
    }
}