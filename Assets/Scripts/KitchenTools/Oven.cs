using Interactibles;
using KitchenTools;
using UnityEngine;

public class Oven : KitchenTool
{
    [SerializeField] private Item.Item debugItem;
    [SerializeField] private Door door;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RunTool(debugItem);
        }
    }
    
    public override void ReceiveItem(Item.Item item)
    {
        if (!CanUseItem(item)) return;
/*
        if (itemToCook != null && itemToCook.IsCooked)
        {
            return itemToCook.Item;
        }
  */      
        if (!door.IsOpen)
        {
            door.Interact();
            return;
        }
        RunTool(item);
    }
}
