using UnityEngine;

namespace Item
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemSO itemSO;
        
        public ItemSO ItemSO => itemSO;
    }
}