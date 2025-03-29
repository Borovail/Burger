using System.Collections.Generic;
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
        private List<Ingridient> ingridients;
        
        public bool HasCookedIngridient => receipt.Ingredients.Count <= ingridients.Count;

        public bool CanCookIngridient(Ingridient ingridient)
        {
            return !HasCookedIngridient;
        }

        public void ReceiveIngridient(Ingridient ingridient)
        {
            if (!dish)
            {
                dish = Instantiate(dishPrefab);
                dish.Setup(ingridientPlace, receipt);
            }
            dish.AddIngridient(ingridient);
        }
    }
}