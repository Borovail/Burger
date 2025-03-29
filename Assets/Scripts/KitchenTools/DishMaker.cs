using DefaultNamespace;
using Item;
using UnityEngine;

namespace KitchenTools
{
    public class DishMaker : MonoBehaviour
    {
        [SerializeField] private Receipt receipt;
        [SerializeField] private Transform ingredientPlace;
        [SerializeField] private Dish dishPrefab;

        private Dish dish;

        //DEBUG
        public void Awake()
        {
            dish = Instantiate(dishPrefab);
            dish.Setup(ingredientPlace, receipt);
        }
    }
}