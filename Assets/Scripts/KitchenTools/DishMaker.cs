using System.Collections.Generic;
using DefaultNamespace;
using Item;
using UnityEngine;

namespace KitchenTools
{
    public class DishMaker : MonoBehaviour, IKitchenTool
    {
        [SerializeField] private Receipt receipt;
        [SerializeField] private Transform ingridientPlace;

        private Transform dish;
        private float offset;
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
                dish = new GameObject("dish").transform;
                dish.SetParent(ingridientPlace);
                dish.localPosition = Vector3.zero;
                offset = 0;
            }
            ingridient.transform.SetParent(dish);
            offset += ingridient.Height * 0.5f;
            ingridient.transform.localPosition = Vector3.zero + dish.up * offset;

        }

        public Ingridient GiveIngridient()
        {
            //TODO: 
            dish = null;
            offset = 0;
            return null;
        }
    }
}