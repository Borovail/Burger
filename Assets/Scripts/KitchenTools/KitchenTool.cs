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

        protected CookedItem itemToCook;

        public abstract void ReceiveItem(Item.Item item);

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
            if (itemToCook == null)
            {
                Debug.LogError("No item to cook");
                return;
            }
            itemToCook.Cook();
            OnItemCooked?.Invoke();
        }

        public bool CanUseItem(Item.Item item)
        {

            if (itemToCook != null)
            {
                Debug.Log("Item is already used");
            }
            return item && acceptedItems.Contains(item.ItemSO);
        }
        
        protected void RunTool(Item.Item item)
        {
            itemToCook = new CookedItem(item);
            SetupTimer();
            SetupItem(item);
            CookProvider.Instance.ConvertItem(type, item, null);
            //TODO: add effects and other stuff
        }

        private void SetupTimer()
        {
            if(!timer) return; 
            timer.StartTimer(cookDuration);
        }

        private void SetupItem(Item.Item item)
        {   
            item.transform.SetParent(ingridientPlace);
            item.transform.localPosition = Vector3.zero + ingridientPlace.up * (item.Height * 0.5f);
        }
    }
}