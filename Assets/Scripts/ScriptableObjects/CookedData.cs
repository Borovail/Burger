using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "CookedData", menuName = "Scriptable Objects/CookedData")]
public class CookedData : ScriptableObject
{
    [SerializeField] private List<CookedItemData> cookedItemsData;

    public CookedItemData? GetItemByType(ItemType itemType)
    {
        foreach (CookedItemData cookedItemData in cookedItemsData)
        {
            if(cookedItemData.ItemType == itemType) return cookedItemData;
        }

        return null;
    }
}
