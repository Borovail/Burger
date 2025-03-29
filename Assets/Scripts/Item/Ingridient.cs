using UnityEngine;

namespace Item
{
    public class Ingridient : MonoBehaviour
    {
        [SerializeField] private ItemSO itemSO;
        private float height;
        private bool isCooked;
        public bool IsCooked => isCooked;


        public ItemSO ItemSO => itemSO;
        public float Height => height;

        private void Awake()
        {
            height = GetComponent<Collider>().bounds.size.y;
        }

        public void Cook()
        {
            isCooked = true;
        }
    }
}