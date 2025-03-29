using System;
using Assets.Scripts.Interactions;
using UnityEngine;

public interface IPickable
{
    event Action OnPickedUp;
    GameObject GetGameObject();
    Rigidbody GetRigidbody();
    bool CanPickUp();
    void PickUp();
}

public interface IInteractableHighlight
{
    public Highlightable GetHighlighter();
    bool CanInteract(GameObject item);
    GameObject Interact(GameObject item);
}