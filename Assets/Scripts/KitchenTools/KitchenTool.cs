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
    [RequireComponent(typeof(Highlightable))]
    public abstract class KitchenTool : MonoBehaviour, IKitchenTool, IInteractable
    {
        [SerializeField] private ToolType type;
        [SerializeField] private float cookDuration = 5f;
        [SerializeField] private Transform ingredientPlace;
        [SerializeField] private UITimer timer;
        [SerializeField] private List<IngredientType> acceptedItems;
        [SerializeField] private TriggerProvider trigger;
        [SerializeField] protected List<Ingredient> ingredientsToCook;

        protected bool isCooking;
        
        public abstract void Interact();

        private void Awake()
        {
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
                if (CanCookIngredient(ingredient))
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
        
        private void TimerOnOnTimerComplete()
        {
            isCooking = false;
            if (ingredientsToCook.Count == 0)
            {
                Debug.LogError("No item to cook");
                return;
            }
            
            foreach (var ingredient in ingredientsToCook)
            {
                CookProvider.Instance.ConvertItem(type, ingredient);
                ingredient.Cook();
                ingredient.EnablePickUp();
            }
            ingredientsToCook.Clear();
        }


        public virtual bool CanCookIngredient(Ingredient ingredient)
        {
            return ingredient && acceptedItems.Contains(ingredient.Type);
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
            //TODO: add effects and other stuff
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
                ingredient.DisablePickUp();
            }
        }
    }
}