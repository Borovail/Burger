using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Receipt", menuName = "Scriptable Objects/Receipt")]
    public class Receipt : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private List<ItemType> ingridients;
        
        public List<ItemType> Ingredients => ingridients;
        public string Title => title;
        public string Description => description;
    }
}