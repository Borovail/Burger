using System;
using System.Collections.Generic;
using Assets.Scripts.Interactions;
using DefaultNamespace;
using Interfaces;
using Item;
using Timers;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace KitchenTools
{
    public abstract class KitchenTool : Highlightable, IKitchenTool, IInteractable
    {
        [SerializeField] protected ToolType type;
        [SerializeField] protected float cookDuration = 5f;
        [SerializeField] private UITimer timer;
        [SerializeField] private List<IngredientType> acceptedItems;
        [SerializeField] private TriggerProvider trigger;
        [SerializeField] protected List<Ingredient> ingredientsToCook;

        protected bool isCooking;
        
        public abstract void Interact();

        protected override void Awake()
        {
            base.Awake();
            if (timer)
            {
                timer.OnTimerComplete += TimerOnOnTimerComplete;
            }
            trigger.OnEnter += TriggerOnOnEnter;
            trigger.OnExit += TriggerOnOnExit;

        }

        private void TriggerOnOnEnter(Collider other)
        {
            if (other.CompareTag(Tags.Ingredient))
            {
                Ingredient ingredient = other.GetComponent<Ingredient>();
                if (CanCookIngredient(ingredient) && !ingredientsToCook.Contains(ingredient))
                {
                    ingredientsToCook.Add(ingredient);
                }
            }
        }

        private void TriggerOnOnExit(Collider other)
        {
            if (other.CompareTag(Tags.Ingredient))
            {
                Ingredient ingredient = other.GetComponent<Ingredient>();
                if (ingredientsToCook.Contains(ingredient))
                {
                    ingredientsToCook.Remove(ingredient);
                }

                if (ingredientsToCook.Count == 0 && timer.IsVisible)
                {
                    timer.Hide();
                }
            }
        }

        protected virtual void TimerOnOnTimerComplete()
        {
            isCooking = false;
            foreach (Ingredient ingredient in ingredientsToCook)
            {
                ingredient.GetRigidbody().isKinematic = false;
                ingredient.EnablePickUp();
            }   
        }


        public virtual bool CanCookIngredient(Ingredient ingredient)
        {
            return ingredient && (acceptedItems.Contains(IngredientType.Any) || acceptedItems.Contains(ingredient.Type));
        }

        protected virtual bool CanRunTool()
        {
            if(ingredientsToCook.Count == 0) return false;
            if(isCooking) return false;
            return true;
        }
        
        protected virtual void RunTool()
        {
            isCooking = true;
            SetupTimer();
            SetupIngredients();
        }

        private void SetupTimer()
        {
            if(!timer) return; 
            timer.StartTimer(cookDuration);
        }

        private void SetupIngredients()
        {
            foreach (Ingredient ingredient in ingredientsToCook)
            {
                ingredient.transform.SetParent(transform);
                //ingredient.GetRigidbody().isKinematic = true;
                ingredient.DisablePickUp();
            }
        }
    }
}