using System;
using Assets.Scripts.Interactions;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour // Õ‡ÍËÌÛÚ¸ Ì‡ Í‡ÏÂÛ
{
    public Material highlightMaterial;
    public Transform HandTransform;


    private IInteractable _currentInteractable;
    private GameObject _heldObject;

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (_heldObject == null)
        //{

        if (Physics.Raycast(ray, out hit))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (_currentInteractable != interactable)
                {
                    _currentInteractable.GetHighlighter().Unhighlight();
                    _currentInteractable = interactable;

                    if (_currentInteractable.CanInteract(_heldObject))
                        _currentInteractable.GetHighlighter().Unhighlight();
                }
            }
            else
                _currentInteractable?.GetHighlighter().Unhighlight();
        }
        else
            _currentInteractable.GetHighlighter().Unhighlight();
        //}

        if (Input.GetMouseButtonUp(0) && _currentInteractable != null)
        {
            if (_currentInteractable.CanInteract(_heldObject))
                PickupObject(_currentInteractable.Interact(_heldObject));
        }
        if (Input.GetMouseButtonUp(1) && _heldObject!=null)
        {
            DropObject();
        }

    }

    //bool CanInteract()
    //{
    //    if (_heldObject == null && _currentInteractable is Meat)
    //        return true;

    //    if (_heldObject != null && _heldObject.TryGetComponent(out Meat _) && _currentInteractable is Pan)
    //        return true;

    //    if (_heldObject == null && _currentInteractable is Pan pan && pan.HaveMeat())
    //        return true;

    //    return false;
    //}


    //void Interact()
    //{
    //    if (_heldObject == null && _currentInteractable is Meat otherMeat)
    //    {
    //        PickupObject(otherMeat);
    //    }

    //    else if (_heldObject != null && _heldObject.TryGetComponent(out Meat meat) && _currentInteractable is Pan pan && !pan.HaveMeat())
    //    {
    //        DropObject();
    //        pan.Start—ooking(meat);
    //    }

    //    else if (_heldObject == null && _currentInteractable is Pan otherPan)
    //    {
    //        PickupObject(otherPan.StopCooking());
    //    }
    //}

    void PickupObject(GameObject objectToPick)
    {
        _heldObject = objectToPick;

        _heldObject.GetComponent<Rigidbody>().isKinematic = true;
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