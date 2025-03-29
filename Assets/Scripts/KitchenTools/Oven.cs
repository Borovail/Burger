using KitchenTools;
using UnityEngine;

public class Oven : KitchenTool
{
    [SerializeField] private Item.Item debugItem;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RunTool(debugItem);
        }
    }

    
    public override bool ReceiveItem(Item.Item item)
    {
        if (!CanUseItem(item)) return false;
        
        RunTool(item);
        
        return true;
    }
}
