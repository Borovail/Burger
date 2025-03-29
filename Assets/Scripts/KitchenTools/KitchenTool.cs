using System;
using System.Collections.Generic;
using Timers;
using UnityEngine;

namespace KitchenTools
{
    public abstract class KitchenTool : MonoBehaviour, IKitchenTool
    {
        [SerializeField] private float cookDuration = 5f;
        [SerializeField] private Transform ingridientPlace;
        [SerializeField] private UITimer timer;
        [SerializeField] private List<ItemSO> acceptedItems;
        public abstract bool ReceiveItem(Item.Item item);

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
            OnItemCooked?.Invoke();
        }

        protected bool CanUseItem(Item.Item item)
        {
            return acceptedItems.Contains(item.ItemSO);
        }
        
        protected void RunTool(Item.Item item)
        {
            SetupTimer();
            SetupItem(item);
            //TODO: add effects and other stuff
        }

        private void SetupTimer()
        {
            if(timer == null) return; 
            timer.StartTimer(cookDuration);
        }

        private void SetupItem(Item.Item item)
        {   
            item.transform.SetParent(ingridientPlace);
            item.transform.localPosition = Vector3.zero + ingridientPlace.up * (item.Height * 0.5f);
        }
    }
}