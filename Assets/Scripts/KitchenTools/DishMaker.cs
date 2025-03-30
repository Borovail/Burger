using DefaultNamespace;
using Interactibles;
using Item;
using UnityEngine;
using UnityEngine.Serialization;

namespace KitchenTools
{
    public class DishMaker : MonoBehaviour
    {
        [SerializeField] private ProcessOrder processOrder;
        [SerializeField] private Transform dishPlace;
        [SerializeField] private Dish dishPrefab;

        private Dish dish;

        //DEBUG
        public void Awake()
        {
            processOrder.OnOrderGet += ProcessOrderOnOnOrderGet;
        }

        private void ProcessOrderOnOnOrderGet(Receipt receipt)
        {
            if (dish)
            {
                Destroy(dish.gameObject);
            }
            dish = Instantiate(dishPrefab);
            dish.Setup(dishPlace, receipt);
        }
    }
}