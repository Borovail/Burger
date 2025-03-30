using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "CookedData", menuName = "Scriptable Objects/CookedData")]
public class IngredientsData : ScriptableObject
{
    [SerializeField] private List<IngredientData> data;

    public IngredientData? GetItemByType(IngredientType ingredientType)
    {
        foreach (IngredientData ingredientData in data)
        {
            if (ingredientData.IngredientType == ingredientType) return ingredientData;
        }

        return null;
    }
}
