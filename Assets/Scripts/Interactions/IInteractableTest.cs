using UnityEngine;

namespace Assets.Scripts.Interactions
{
    public interface IInteractableTest
    {
        bool CanInteract(GameObject heldObject);
        void Highlight();
        void Unhighlight();
    }

}