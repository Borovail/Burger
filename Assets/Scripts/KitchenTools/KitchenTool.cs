using System;
using System.Collections.Generic;
using Timers;
using UnityEngine;

namespace KitchenTools
{
    public abstract class KitchenTool : MonoBehaviour, IKitchenTool
    {
        [SerializeField] private float cookDuration = 5f;
        [SerializeField] private UITimer timer;
        [SerializeField] private List<Item> acceptedItems;
        public abstract bool ReceiveItem(Item item);

        public event Action OnItemCooked;

        private void Awake()
        {
            timer.OnTimerComplete += TimerOnOnTimerComplete;
        }

        private void TimerOnOnTimerComplete()
        {
            OnItemCooked?.Invoke();
        }

        protected bool CanUseItem(Item item)
        {
            return acceptedItems.Contains(item);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                RunTool();
            }
        }
        
        protected void RunTool()
        {
            SetupTimer();
            //TODO: add effects and other stuff
        }

        private void SetupTimer()
        {
            timer.StartTimer(cookDuration);
        }
    }
}