using System;
using Assets.Scripts.Interactions;
using Item;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public Transform HandTransform;


    private IHighlightable _currentHighlightable;
    private GameObject _heldObject;

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (_heldObject == null)
        //{

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out IHighlightable highlightable))
            {

                if (_currentHighlightable != highlightable)
                {
                    _currentHighlightable?.Unhighlight();
                    _currentHighlightable = highlightable;

                    if (CanInteract())
                        _currentHighlightable.Highlight();
                }
            }
            else
            {
                _currentHighlightable?.Unhighlight();
                _currentHighlightable = null;
            }
        }
        else
        {
            _currentHighlightable?.Unhighlight();
            _currentHighlightable = null;
        }
        //}

        if (Input.GetMouseButtonUp(0) && _currentHighlightable != null)
        {
            if (CanInteract())
                Interact();
        }

        if (Input.GetMouseButtonUp(1) && _heldObject != null)
        {
            DropObject();
        }

    }

    bool CanInteract()
    {

        if (_heldObject == null && _currentHighlightable.GetGameObject().TryGetComponent(out IPickable _))
            return true;

        if (_heldObject != null && _heldObject.TryGetComponent(out Ingridient ingridient)
            && _currentHighlightable.GetGameObject().TryGetComponent(out IKitchenTool kitchenTool) && kitchenTool.CanCookIngridient(ingridient))
            return true;

        if (_heldObject == null && _currentHighlightable.GetGameObject().TryGetComponent(out IKitchenTool otherKitchenTool) && otherKitchenTool.HasCookedIngridient)
            return true;

        return false;
    }


    void Interact()
    {
        if (_heldObject == null && _currentHighlightable.GetGameObject().TryGetComponent(out IPickable pickable))
        {
            PickupObject(pickable);
        }
        else if (_heldObject != null)
        {
            DropObject();
        }
    }

    void PickupObject(IPickable objectToPick)
    {
        _heldObject = objectToPick.GetGameObject();

        objectToPick.GetRigidbody().isKinematic = true;
        _heldObject.transform.SetParent(HandTransform);
        _heldObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    void DropObject()
    {
        _heldObject.transform.SetParent(null);
        _heldObject.GetComponent<Rigidbody>().isKinematic = false;
        _heldObject = null;
    }

}