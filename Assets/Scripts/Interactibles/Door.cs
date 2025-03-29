using DG.Tweening;
using Interfaces;
using UnityEngine;

namespace Interactibles
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform door;
        [SerializeField] private Vector3 doorOpenRotation;
        [SerializeField] private float doorRotateDuration;
        private bool isOpen = false;
        private Vector3 doorInitialRotation;

        public bool IsOpen => isOpen;
        
        private void Start()
        {
            doorInitialRotation = door.localEulerAngles;
        }

        public void Interact()
        {
            isOpen = !isOpen;
            if (isOpen)
            {
                door.DOLocalRotate(doorOpenRotation, doorRotateDuration);
            }
            else
            {
                door.DOLocalRotate(doorInitialRotation, doorRotateDuration);
            }

        }
    }
}