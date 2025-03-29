using UnityEngine;

namespace Utils
{
    using UnityEngine;
    using System;

    public class TriggerProvider : MonoBehaviour
    {
        public event Action<Collider> OnEnter;
        public event Action<Collider> OnStay;
        public event Action<Collider> OnExit;

        private void OnTriggerEnter(Collider other)
        {
            OnEnter?.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            OnStay?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnExit?.Invoke(other);
        }
    }
}