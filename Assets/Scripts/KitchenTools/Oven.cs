using Interfaces;
using KitchenTools;
using UnityEngine;

public class Oven : KitchenTool, IInteractable
{
    [SerializeField] private Transform door;
    public override bool ReceiveItem(Item item)
    {
        if (!CanUseItem(item)) return false;
        return true;
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}
