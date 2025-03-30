using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public struct IngredientData
    {
        [SerializeField] private IngredientType ingredientType;
        [SerializeField] private Mesh cookedMesh;
        [SerializeField] private Material[] cookedMaterials;
        [SerializeField] private Sprite icon;

        public IngredientType IngredientType => ingredientType;
        public Mesh CookedMesh => cookedMesh;
        public Material[] CookedMaterials => cookedMaterials;
        public Sprite Icon => icon;
    }
}