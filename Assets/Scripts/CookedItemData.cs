using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public struct CookedItemData
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private Mesh cookedMesh;
        [SerializeField] private Material[] cookedMaterials;
        
        public ItemType ItemType => itemType;
        public Mesh CookedMesh => cookedMesh;
        public Material[] CookedMaterials => cookedMaterials;
    }
}