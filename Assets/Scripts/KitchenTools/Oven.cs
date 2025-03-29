using Interactibles;
using KitchenTools;
using UnityEngine;

public class Oven : KitchenTool
{
    [SerializeField] private Door door;

    public override void Interact()
    {
        if (CanRunTool())
        {
            RunTool();
        }
    }

    protected override void RunTool()
    {
        if (door.IsOpen)
        {
            door.Interact();
        }
        base.RunTool();
    }
}
