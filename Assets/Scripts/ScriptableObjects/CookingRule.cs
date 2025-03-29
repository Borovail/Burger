using Item;
using KitchenTools;
using UnityEngine;

[CreateAssetMenu(fileName = "CookingRule", menuName = "Scriptable Objects/CookingRule")]
public class CookingRule : ScriptableObject
{
    [SerializeField] private ToolType toolType;
    [SerializeField] private ItemType startIngridient;
    [SerializeField] private ItemType startFlavourIngridient;
    [SerializeField] private ItemSO targetIngridient;
    [SerializeField, Range(0, 1f)] private float similarityPercentage;
    
    public ToolType ToolType => toolType;
    public ItemType StartIngridient => startIngridient;
    public ItemType StartFlavorIngridient => startFlavourIngridient;
    public ItemSO TargetIngridient => targetIngridient;
    public float SimilarityPercentage => similarityPercentage;
}
