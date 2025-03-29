using UnityEngine;

namespace Item
{
    public class Ingridient : MonoBehaviour
    {
        [SerializeField] private ItemSO itemSO;
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
    }
}