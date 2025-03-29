using UnityEngine;

namespace Item
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemSO itemSO;
        private float height;
        
        public ItemSO ItemSO => itemSO;
        public float Height => height;

        private void Awake()
        {
            height = GetComponent<Collider>().bounds.size.y;
        }
    }
}