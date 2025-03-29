using System;
using Assets.Scripts.Interactions;
using DefaultNamespace;
using UnityEngine;

namespace Item
{
    [RequireComponent(typeof(Rigidbody),typeof(Highlightable))]
    public class Ingredient : MonoBehaviour,IPickable
    {
        [SerializeField] private ItemSO itemSO;
        private Rigidbody _rigidbody;

        [SerializeField] private bool isCooked;
        [SerializeField] private ItemType addedFlavour = ItemType.Null;
        [SerializeField] private float similarityPercentage = 1f;
        private float height;
        private bool canBePickedUp = true;
        private MeshFilter filter;
        private MeshRenderer renderer;
        
        public bool IsCooked => isCooked;
        public ItemType AddedFlavour => addedFlavour;

        public ItemSO ItemSO => itemSO;
        public float Height => height;

        private void Awake()
        {
            height = GetComponent<Collider>().bounds.size.y;
            filter = GetComponent<MeshFilter>();
            renderer = GetComponent<MeshRenderer>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void AddFlavour(ItemSO addedFlavour)
        {
            this.addedFlavour = addedFlavour.itemType;
        }

        public void ChangeIngredient(ItemSO itemSo, float similarityPercent)
        {
            addedFlavour = ItemType.Null;
            itemSO = itemSo;
            similarityPercentage = similarityPercent;
        }
        
        public void Cook()
        {
            isCooked = true;
            CookedItemData? cookedItemData = CookProvider.Instance.CookedData.GetItemByType(itemSO.itemType);
            if (cookedItemData != null)
            {
                filter.mesh = cookedItemData.Value.CookedMesh;
                renderer.materials = cookedItemData.Value.CookedMaterials;
            }
        }

        public event Action OnPickedUp;

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public Rigidbody GetRigidbody()
        {
            return _rigidbody;
        }

        public bool CanPickUp()
        {
            return canBePickedUp;
        }

        public void PickUp()
        {
            OnPickedUp?.Invoke();
        }

        public void EnablePickUp()
        {
            canBePickedUp = true;
        }

        public void DisablePickUp()
        {
            canBePickedUp = false;
        }
    }
}