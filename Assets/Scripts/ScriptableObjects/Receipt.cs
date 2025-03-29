using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Receipt", menuName = "Scriptable Objects/Receipt")]
    public class Receipt : ScriptableObject
    {
        [SerializeField] private List<ItemType> ingridients;
        
        public List<ItemType> Ingredients => ingridients;
    }
}