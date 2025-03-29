using UnityEngine;

namespace Assets.Scripts.Interactions
{
    [RequireComponent(typeof(Highlightable))]
    public class Meat : MonoBehaviour,IInteractableHighlight
    {
        private Rigidbody _rigidbody;
        private Highlightable _highlightable;

        private void Awake()
        {
            _highlightable = GetComponent<Highlightable>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public Highlightable GetHighlighter()
        {
            return _highlightable;
        }

        public bool CanInteract(GameObject item)
        {
            return item == null;
        }

        public GameObject Interact(GameObject item)
        {
            return gameObject;
        }
    }
}