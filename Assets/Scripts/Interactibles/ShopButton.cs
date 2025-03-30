using DefaultNamespace;
using Interfaces;
using Item;
using TMPro;
using UnityEngine;

namespace Interactibles
{
    public class ShopButton : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private TextMeshProUGUI priceText;
        
        private ShopItem? currentShopItem;
        private int cost;
        private void Start()
        {
            Setup();
        }
        
        public void Interact()
        {
            if (Player.Instance.HaveMoneyAmount(cost))
            {
                Player.Instance.RemoveMoney(cost);
            }
            Transform obj = Instantiate(currentShopItem.Value.ingredient, spawnPosition);
            obj.transform.GetChild(1).GetComponent<Ingredient>().SetSimilarity(currentShopItem.Value.similarity);
            Setup();
        }

        private void Setup()
        {
            currentShopItem = CookProvider.Instance.ShopMenu.GetShopItem();
            cost = (int)(currentShopItem.Value.baseCost * currentShopItem.Value.similarity);
            priceText.text = $"Price: ${cost}";
        }
    }
}