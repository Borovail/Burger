using Interactibles;
using Item;
using KitchenTools;
using UnityEngine;
using UnityEngine.Serialization;

public class Oven : KitchenTool
{
    [FormerlySerializedAs("debugItem")] [SerializeField] private Item.Ingridient debugIngridient;
    [SerializeField] private Door door;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RunTool(debugIngridient);
        }
    }

    public override bool CanCookIngridient(Ingridient ingridient)
    {
        return door.IsOpen && base.CanCookIngridient(ingridient);
    }

    public override void ReceiveIngridient(Item.Ingridient ingridient)
    {
        if (!CanCookIngridient(ingridient)) return;
        RunTool(ingridient);
    }
}
