using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Objects/ShopMenu")]
public class ShopMenu : ScriptableObject
{
    [SerializeField] private List<ShopItem> shopItems;
    
    public ShopItem GetShopItem()
    {
        int index = Random.Range(0, shopItems.Count);
        float similarity = Random.Range(0.6f, 1f);
        ShopItem shopItem = shopItems[index];
        shopItem.similarity = similarity;
        return shopItem;
    }
}
