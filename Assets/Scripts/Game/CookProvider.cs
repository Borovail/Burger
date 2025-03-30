using System.Collections.Generic;
using Item;
using KitchenTools;
using UnityEngine;

namespace DefaultNamespace
{
    public class CookProvider : MonoBehaviour
    {
        [SerializeField] private List<CookingRule> rules;
        [SerializeField] private IngredientsData ingredientsData;
        public IngredientsData IngredientsData => ingredientsData;

        public static CookProvider Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ConvertItem(ToolType toolType, Ingredient item)
        {
            foreach (CookingRule rule in rules)
            {
                if (rule.ToolType == toolType && item.Type == rule.StartIngredient && item.AddedFlavour == rule.StartFlavorIngredient)
                {
                    item.ChangeIngredient(rule.SimilarityBonus);   
                }
            }
        }

        public bool IsMainIngredient(IngredientType ingredientType)
        {
            switch (ingredientType)
            {
                case IngredientType.Meat:
                case IngredientType.Bread:
                case IngredientType.Salad:
                return true;
            }
            return false;
        }

        public bool IsFlavour(IngredientType ingredientType)
        {
            return ingredientType != IngredientType.Null && !IsMainIngredient(ingredientType);
        }
    }
}