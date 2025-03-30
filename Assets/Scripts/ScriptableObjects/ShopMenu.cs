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
        float similarity = Random.Range(0.2f, 1f);
        ShopItem shopItem = shopItems[index];
        similarity -= 0.25f;
        similarity = Mathf.Clamp(similarity, 0.3f, 1);
        shopItem.similarity = similarity;
        return shopItem;
    }
}
