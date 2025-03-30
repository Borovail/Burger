using Item;
using KitchenTools;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "CookingRule", menuName = "Scriptable Objects/CookingRule")]
public class CookingRule : ScriptableObject
{
    [SerializeField] private ToolType toolType;
    [SerializeField] private IngredientType startIngredient;
    [SerializeField] private IngredientType startFlavourIngredient;
    [SerializeField, Range(0, 1f)] private float similarityBonus;
    
    public ToolType ToolType => toolType;
    public IngredientType StartIngredient => startIngredient;
    public IngredientType StartFlavorIngredient => startFlavourIngredient;
    public float SimilarityBonus => similarityBonus;
}
