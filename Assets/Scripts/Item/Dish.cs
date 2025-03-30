using System;
using System.Collections.Generic;
using Assets.Scripts.Interactions;
using DefaultNamespace;
using DefaultNamespace.PopUp;
using UnityEngine;
using Utils;

namespace Item
{
    public class Dish : Highlightable, IPickable
    {
        [SerializeField] private List<Ingredient> ingredients;
        [SerializeField] private TriggerProvider triggerProvider;
        [SerializeField] private ExpirationPopUp ui;
        private float offset;
        private Receipt receipt;
        private Rigidbody rigidbody;

        public bool IsFull => receipt.Ingredients.Count <= ingredients.Count;

        //TODO: add similarity view, calculations

        protected override void Awake()
        {
            base.Awake();
            rigidbody = GetComponent<Rigidbody>();
            triggerProvider.OnEnter += TriggerProviderOnOnEnter;
            triggerProvider.OnExit += TriggerProviderOnOnExit;

        }
        
        private void TriggerProviderOnOnExit(Collider other)
        {
            if (other.CompareTag(Tags.Ingredient))
            {
                RemoveIngredient(other.GetComponent<Ingredient>());
            }
        }

        private void TriggerProviderOnOnEnter(Collider other)
        {
            if (other.CompareTag(Tags.Ingredient))
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
            ui.Hide();
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
                ingredient.GetRigidbody().constraints = RigidbodyConstraints.None;
                ingredient.GetRigidbody().isKinematic = false;
            }
        }

        public override void Highlight()
        {
            base.Highlight();
            
            //TODO: should do this every ui show, gotta be careful
            float similarity = CalculateSimilarity();
            ui.UpdateFillAmount(similarity);
            ui.Show();
        }

        public override void Unhighlight()
        {
            base.Unhighlight();
            ui.Hide();
        }

        private float CalculateSimilarity()
        {
            float total = 0;
            float similarity = 0;
            if(receipt == null) return 0.95f;
            for (int i = 0; i < receipt.Ingredients.Count; i++)
            {
                total += 1;
                if (i > ingredients.Count - 1) continue;
                
                if (ingredients[i].Type == receipt.Ingredients[i] && ingredients[i].IsCooked)
                {
                    similarity += ingredients[i].SimilarityPercentage;
                }
            }
            return similarity / total;
        }

        private void OnDestroy()
        {
            foreach (Ingredient ingredient in ingredients)
            {
                Destroy(ingredient.GetGameObject());
            }
        }
    }
}