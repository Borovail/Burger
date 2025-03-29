using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Utils;

namespace Item
{
    public class Dish : MonoBehaviour, IPickable
    {
        [SerializeField] private List<Ingredient> ingredients;
        [SerializeField] private TriggerProvider triggerProvider;
        private float offset;
        private Receipt receipt;
        private Rigidbody rigidbody;

        public bool IsFull => receipt.Ingredients.Count <= ingredients.Count;

        //TODO: add similarity view, calculations

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            triggerProvider.OnEnter += TriggerProviderOnOnEnter;
            triggerProvider.OnExit += TriggerProviderOnOnExit;

        }
        
        private void TriggerProviderOnOnExit(Collider other)
        {
            if (other.CompareTag(Tags.Ingridient))
            {
                RemoveIngredient(other.GetComponent<Ingredient>());
            }
        }

        private void TriggerProviderOnOnEnter(Collider other)
        {
            if (other.CompareTag(Tags.Ingridient))
            {
                Ingredient ingredient = other.GetComponent<Ingredient>();
                if (!IsFull)
                {
                    AddIngredient(ingredient);
                }
            }
        }

        
        public void Setup(Transform parent, Receipt receipt)
        {
            this.receipt = receipt;
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
        }
        
        public void AddIngredient(Ingredient ingredient)
        {
            ingredient.GetRigidbody().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            ingredients.Add(ingredient);
        }

        public void RemoveIngredient(Ingredient ingredient)
        {
            ingredient.GetRigidbody().constraints = RigidbodyConstraints.None;
            ingredients.Remove(ingredient);
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
            return true;
        }

        public void PickUp()
        {
            foreach (Ingredient ingredient in ingredients)
            {
                ingredient.transform.SetParent(transform);
                ingredient.GetRigidbody().isKinematic = true;
            }
            OnPickedUp?.Invoke();
        }

        public void Drop()
        {
            GetRigidbody().isKinematic = false;
            foreach (Ingredient ingredient in ingredients)
            {
                //ingredient.transform.SetParent(null);
                ingredient.GetRigidbody().constraints = RigidbodyConstraints.None;
                ingredient.GetRigidbody().isKinematic = false;
            }
        }
    }
}