using Assets.Scripts.Interactions;
using UnityEngine;

public interface IPickable
{
    GameObject GetGameObject();
    Rigidbody GetRigidbody();
}

public interface IInteractableHighlight
{
    public Highlightable GetHighlighter();
    bool CanInteract(GameObject item);
    GameObject Interact(GameObject item);
}