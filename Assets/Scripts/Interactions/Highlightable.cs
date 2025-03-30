using UnityEngine;

namespace Assets.Scripts.Interactions
{
    public interface IHighlightable
    {
        GameObject GetGameObject();
        void Highlight();
        void Unhighlight();
    }


    public class Highlightable : MonoBehaviour,IHighlightable
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Material _highlightMaterial;

        private void Awake()
        {
            if (_renderer != null) return;
            if (!TryGetComponent(out _renderer))
            {
                _renderer = GetComponentInChildren<Renderer>();
            }
        }


        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public virtual void Highlight()
        {
            // Создаем новый массив материалов, который включает подсветку
            Material[] materials = new Material[_renderer.materials.Length + 1];
            for (int i = 0; i < _renderer.materials.Length; i++)
            {
                materials[i] = _renderer.materials[i];
            }
            materials[materials.Length - 1] = _highlightMaterial; // Добавляем материал подсветки
            _renderer.materials = materials; // Применяем новый массив материалов
        }

        public virtual void Unhighlight()
        {
            Material[] materials = _renderer.materials;

            // Если материалов больше одного, то удаляем последний (материал подсветки)
            if (materials.Length > 1)
            {
                var newMaterials = new Material[materials.Length - 1];
                for (int i = 0; i < materials.Length - 1; i++) // Копируем все кроме последнего
                {
                    newMaterials[i] = materials[i];
                }
                _renderer.materials = newMaterials;
            }
        }
    }
}