using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Item
{
    public class Dish : MonoBehaviour
    {
        [SerializeField] private List<Ingridient> ingridients;
        private float offset;
        private Receipt receipt;
        
        //TODO: add similarity view, calculations

        public void Setup(Transform parent, Receipt receipt)
        {
            this.receipt = receipt;
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
        }
        
        public void AddIngridient(Ingridient ingridient)
        {
            ingridients.Add(ingridient);
            ingridient.transform.SetParent(transform);
            offset += ingridient.Height * 0.5f;
            ingridient.transform.localPosition = Vector3.zero + transform.up * offset;
        }

        public void RemoveIngridient(Ingridient ingridient)
        {
            ingridients.Remove(ingridient);
        }
    }
}