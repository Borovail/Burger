using DefaultNamespace;
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

    protected override void TimerOnOnTimerComplete()
    {
        //TODO: Will be used also in pan (should move to other class)
        base.TimerOnOnTimerComplete();
        if (ingredientsToCook.Count == 0)
        {
            Debug.LogError("No item to cook");
            return;
        }
        
        foreach (var ingredient in ingredientsToCook)
        {
            CookProvider.Instance.ConvertItem(type, ingredient);
            ingredient.Cook();
        }
        ingredientsToCook.Clear();
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
