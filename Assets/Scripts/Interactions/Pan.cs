using UnityEngine;

namespace Assets.Scripts.Interactions
{
    public class Pan : MonoBehaviour,IInteractableHighlight
    {
        public Transform Center;
        private MeshRenderer _renderer;
        private Meat _meat;
        private Highlightable _highlightable;

        private void Awake()
        {
            _highlightable = GetComponent<Highlightable>();
            _renderer = GetComponent<MeshRenderer>();
        }


        public void StartСooking(Meat meat)
        {
            _meat = meat;
            _meat.GetComponent<Collider>().enabled = false;
            _meat.GetComponent<Rigidbody>().isKinematic = true;

            _meat.transform.SetParent(Center);
            _meat.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public Meat StopCooking()
        {
            _meat.GetComponent<Collider>().enabled = true;
            _meat.transform.SetParent(null);
            var meat = _meat;
            _meat = null;
            return meat;
        }

        public Highlightable GetHighlighter()
        {
            throw new System.NotImplementedException();
        }

        public bool CanInteract(GameObject item)
        {
            if (item == null && _meat != null) return true;

            if (_meat == null && item != null && item.TryGetComponent(out Meat meat)) return true;

            return false;
        }

        public GameObject Interact(GameObject item)
        {
            throw new System.NotImplementedException();
        }
    }
}