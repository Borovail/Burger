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

        private float height;
        private bool isCooked;
        public bool IsCooked => isCooked;


        public ItemSO ItemSO => itemSO;
        public float Height => height;

        private void Awake()
        {
            height = GetComponent<Collider>().bounds.size.y;
            _rigidbody = GetComponent<Rigidbody>();
            _highlightable = GetComponent<Highlightable>();
        }

        public void Cook()
        {
            isCooked = true;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public Rigidbody GetRigidbody()
        {
            return _rigidbody;
        }
    }
}