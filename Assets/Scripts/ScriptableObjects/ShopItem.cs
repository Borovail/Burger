using System;
using Item;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public struct ShopItem
    {
        public Transform ingredient;
        public int baseCost;
        public float similarity;
    }
}