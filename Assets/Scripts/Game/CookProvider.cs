using System.Collections.Generic;
using Item;
using KitchenTools;
using UnityEngine;

namespace DefaultNamespace
{
    public class CookProvider : MonoBehaviour
    {
        [SerializeField] private List<CookingRule> rules;
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

        public void ConvertItem(ToolType toolType, Ingridient item)
        {
            foreach (CookingRule rule in rules)
            {
                if (rule.ToolType == toolType && item.ItemSO.itemType == rule.StartIngridient && item.AddedFlavour == rule.StartFlavorIngridient)
                {
                    item.ChangeIngridient(rule.TargetIngridient, rule.SimilarityPercentage);   
                }
            }
        }
    }
}