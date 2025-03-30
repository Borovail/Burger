using Assets.Scripts.Interactions;
using Interfaces;
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

        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (_heldObject && Mathf.Abs(scrollDelta) > 0)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                float rotationSpeed = 40f;
                _heldObject.transform.Rotate(Vector3.right, scrollDelta * rotationSpeed);
            }
            else
            {
                float moveSpeed = 1f;
                _heldObject.transform.localPosition += Vector3.forward * (scrollDelta * moveSpeed);
            }
        }
    }

    bool CanInteract()
    {
        if (_currentHighlightable.GetGameObject().TryGetComponent(out IInteractable interactable))
        {
            return true;
        }

        if (_heldObject == null && _currentHighlightable.GetGameObject().TryGetComponent(out IPickable pickable))
            return pickable.CanPickUp();

        if (_heldObject != null && _heldObject.TryGetComponent(out Ingredient ingridient)
            && _currentHighlightable.GetGameObject().TryGetComponent(out IKitchenTool kitchenTool) && kitchenTool.CanCookIngredient(ingridient))
            return true;

        // if (_heldObject == null && _currentHighlightable.GetGameObject().TryGetComponent(out IKitchenTool otherKitchenTool) && otherKitchenTool.HasCookedIngridient)
        //     return true;

        return false;
    }


    void Interact()
    {
        if (_currentHighlightable.GetGameObject().TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact();
        }
        else if (_currentHighlightable.GetGameObject().TryGetComponent(out IPickable pickable) && _heldObject == null)
        {
            pickable.PickUp();
            PickupObject(pickable);
        }
        else if (_heldObject != null)
        {
            pickable.Drop();
            DropObject();
        }
    }

    void PickupObject(IPickable objectToPick)
    {
        _heldObject = objectToPick.GetGameObject();

        objectToPick.GetRigidbody().isKinematic = true;
        HandTransform.position = _heldObject.transform.position;
        _heldObject.transform.SetParent(HandTransform);
    }

    void DropObject()
    {
        if (_heldObject.TryGetComponent(out IPickable pickable))
        {
            pickable.Drop();
        }
        _heldObject.transform.SetParent(null);
        _heldObject.GetComponent<Rigidbody>().isKinematic = false;
        _heldObject = null;
    }

}