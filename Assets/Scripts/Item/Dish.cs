using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Item
{
    public class Dish : MonoBehaviour, IPickable
    {
        [SerializeField] private List<Ingredient> ingridients;
        private float offset;
        private Receipt receipt;
        private Rigidbody rigidbody;
        
        public List<Ingredient> Ingredients => ingridients;
        
        //TODO: add similarity view, calculations

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        
        public void Setup(Transform parent, Receipt receipt)
        {
            this.receipt = receipt;
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
        }
        
        public void AddIngridient(Ingredient ingredient)
        {
            ingredient.GetRigidbody().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationZ;
            ingridients.Add(ingredient);
            ingredient.transform.SetParent(transform);
            offset += ingredient.Height * 0.5f;
            ingredient.transform.localPosition = Vector3.zero + transform.up * (offset + 2);
        }

        public void RemoveIngridient(Ingredient ingredient)
        {
            offset -= ingredient.Height * 0.5f;
            ingridients.Remove(ingredient);
        }

        public event Action OnPickedUp;

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public Rigidbody GetRigidbody()
        {
            return rigidbody;
        }

        public bool CanPickUp()
        {
            return receipt.Ingredients.Count <= ingridients.Count;
        }

        public void PickUp()
        {
            OnPickedUp?.Invoke();
        }
    }
}