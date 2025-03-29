using System;
using System.Collections.Generic;
using Assets.Scripts.Interactions;
using DefaultNamespace;
using Interfaces;
using Item;
using Timers;
using UnityEngine;
using Utils;

namespace KitchenTools
{
    [RequireComponent(typeof(Highlightable))]
    public abstract class KitchenTool : MonoBehaviour, IKitchenTool, IInteractable
    {
        [SerializeField] private ToolType type;
        [SerializeField] private float cookDuration = 5f;
        [SerializeField] private Transform ingridientPlace;
        [SerializeField] private UITimer timer;
        [SerializeField] private List<ItemSO> acceptedItems;
        [SerializeField] private TriggerProvider trigger;
        [SerializeField] protected List<Ingredient> ingridientsToCook;

        protected bool isCooking;
        
        public abstract void Interact();
        public event Action OnItemCooked;

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
            if (other.CompareTag(Tags.Ingridient))
            {
                Ingredient ingredient = other.GetComponent<Ingredient>();
                if (CanCookIngredient(ingredient))
                {
                    ingridientsToCook.Add(ingredient);
                }
            }
        }

        private void TriggerOnOnExit(Collider other)
        {
            if (other.CompareTag(Tags.Ingridient))
            {
                Ingredient ingredient = other.GetComponent<Ingredient>();
                if (ingridientsToCook.Contains(ingredient))
                {
                    ingridientsToCook.Remove(ingredient);
                }
            }
        }
        
        private void TimerOnOnTimerComplete()
        {
            isCooking = false;
            if (ingridientsToCook.Count == 0)
            {
                Debug.LogError("No item to cook");
                return;
            }
            
            foreach (var ingridient in ingridientsToCook)
            {
                CookProvider.Instance.ConvertItem(type, ingridient);
                ingridient.Cook();
                ingridient.EnablePickUp();
            }
            ingridientsToCook.Clear();
            OnItemCooked?.Invoke();
        }


        public virtual bool CanCookIngredient(Ingredient ingredient)
        {
            return ingredient && acceptedItems.Contains(ingredient.ItemSO);
        }

        protected virtual bool CanRunTool()
        {
            if(ingridientsToCook.Count == 0) return false;
            if(isCooking) return false;
            return true;
        }
        
        protected virtual void RunTool()
        {
            isCooking = true;
            SetupTimer();
            SetupIngridients();
            //TODO: add effects and other stuff
        }

        private void SetupTimer()
        {
            if(!timer) return; 
            timer.StartTimer(cookDuration);
        }

        private void SetupIngridients()
        {
            foreach (Ingredient ingridient in ingridientsToCook)
            {
                ingridient.DisablePickUp();
            }
        }
    }
}