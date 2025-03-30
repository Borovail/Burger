using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public struct Receipt
    {
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private List<IngredientRecipeType> ingredients;
        [SerializeField] private int baseCost;
        
        public List<IngredientRecipeType> Ingredients => ingredients;
        public string Title => title;
        public string Description => description;
        public int BaseCost => baseCost;
    }
}