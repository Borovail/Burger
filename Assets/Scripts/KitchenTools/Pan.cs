using DefaultNamespace;
using UnityEngine;

namespace KitchenTools
{
    public class Pan : KitchenTool
    {
        public override void Interact()
        {
            if (CanRunTool())
            {
                RunTool();
            }
        }
        
        protected override void TimerOnOnTimerComplete()
        {
            base.TimerOnOnTimerComplete();
            if (ingredientsToCook.Count == 0)
            {
                Debug.LogError("No item to cook");
                return;
            }
        
            foreach (var ingredient in ingredientsToCook)
            {
                CookProvider.Instance.ConvertItem(type, ingredient);
                ingredient.Cook(type);
            }
            ingredientsToCook.Clear();
        }
    }
}