using System;
using System.Collections.Generic;
using DefaultNamespace;
using Item;
using Timers;
using UnityEngine;

namespace KitchenTools
{
    public abstract class KitchenTool : MonoBehaviour, IKitchenTool
    {
        [SerializeField] private ToolType type;
        [SerializeField] private float cookDuration = 5f;
        [SerializeField] private Transform ingridientPlace;
        [SerializeField] private UITimer timer;
        [SerializeField] private List<ItemSO> acceptedItems;

        protected Item.Ingridient IngridientToCook;

        public bool HasCookedIngridient => IngridientToCook != null && IngridientToCook.IsCooked;

        public abstract void ReceiveIngridient(Ingridient ingridient);
        public Ingridient GiveIngridient()
        {
            return IngridientToCook;
        }

        public event Action OnItemCooked;

        private void Awake()
        {
            if (timer)
            {
                timer.OnTimerComplete += TimerOnOnTimerComplete;
            }
        }

        private void TimerOnOnTimerComplete()
        {
            if (IngridientToCook == null)
            {
                Debug.LogError("No item to cook");
                return;
            }
            IngridientToCook.Cook();
            OnItemCooked?.Invoke();
        }


        public virtual bool CanCookIngridient(Ingridient ingridient)
        {
            if (IngridientToCook != null)
            {
                Debug.Log("Item is already used");
            }
            return ingridient && acceptedItems.Contains(ingridient.ItemSO);
        }
        
        protected void RunTool(Ingridient ingridient)
        {
            IngridientToCook = ingridient;
            SetupTimer();
            SetupItem(ingridient);
            CookProvider.Instance.ConvertItem(type, ingridient);
            //TODO: add effects and other stuff
        }

        private void SetupTimer()
        {
            if(!timer) return; 
            timer.StartTimer(cookDuration);
        }

        private void SetupItem(Ingridient ingridient)
        {   
            ingridient.transform.SetParent(ingridientPlace);
            ingridient.transform.localPosition = Vector3.zero + ingridientPlace.up * (ingridient.Height * 0.5f);
        }
    }
}