using System;
using KitchenTools;

namespace DefaultNamespace
{
    [Serializable]
    public struct IngredientRecipeType
    {
        public IngredientType Type;
		public ToolType Tool;
    }
}