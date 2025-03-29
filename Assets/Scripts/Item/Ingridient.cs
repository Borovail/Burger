using System;
using Assets.Scripts.Interactions;
using UnityEngine;

namespace Item
{
    [RequireComponent(typeof(Rigidbody),typeof(Highlightable))]
    public class Ingridient : MonoBehaviour,IPickable
    {
        [SerializeField] private ItemSO itemSO;
        private Rigidbody _rigidbody;
        private Highlightable _highlightable;

        [SerializeField] private bool isCooked;
        [SerializeField] private ItemType addedFlavour = ItemType.Null;
        [SerializeField] private float similarityPercentage = 1f;
        private float height;
        
        public bool IsCooked => isCooked;
        public ItemType AddedFlavour => addedFlavour;

        public ItemSO ItemSO => itemSO;
        public float Height => height;

        private void Awake()
        {
            height = GetComponent<Collider>().bounds.size.y;
            _rigidbody = GetComponent<Rigidbody>();
            _highlightable = GetComponent<Highlightable>();
        }

        public void AddFlavour(ItemSO addedFlavour)
        {
            this.addedFlavour = addedFlavour.itemType;
        }

        public void ChangeIngridient(ItemSO itemSo, float similarityPercent)
        {
            addedFlavour = ItemType.Null;
            itemSO = itemSo;
            similarityPercentage = similarityPercent;
        }
        
        public void Cook()
        {
            isCooked = true;
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
            return true;
        }

        public void PickUp()
        {
            OnPickedUp?.Invoke();
        }
    }
}