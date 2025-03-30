using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Receipt", menuName = "Scriptable Objects/Receipt")]
    public class Receipt : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private List<IngredientType> ingredients;
        
        public List<IngredientType> Ingredients => ingredients;
        public string Title => title;
        public string Description => description;
    }
}