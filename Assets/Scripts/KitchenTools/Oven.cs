using DG.Tweening;
using Interfaces;
using KitchenTools;
using UnityEngine;

public class Oven : KitchenTool, IInteractable
{
    
    [SerializeField] private Transform door;
    [SerializeField] private Vector3 doorOpenRotation;
    [SerializeField] private float doorRotateDuration;
    
    private bool IsOpen = false;
    private Vector3 doorInitialRotation;
    private void Start()
    {
        doorInitialRotation = door.localEulerAngles;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Interact();
        }
    }

    public override bool ReceiveItem(Item.Item item)
    {
        if (!CanUseItem(item)) return false;
        
        
        
        return true;
    }

    public void Interact()
    {
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            door.DOLocalRotate(doorOpenRotation, doorRotateDuration);
        }
        else
        {
            door.DOLocalRotate(doorInitialRotation, doorRotateDuration);
        }
    }
}
