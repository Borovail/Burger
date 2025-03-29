using UnityEngine;

namespace Assets.Scripts.Interactions
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Highlightable : MonoBehaviour
    {
        private MeshRenderer _renderer;
        [SerializeField] private Material _highlightMaterial;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }
        public void Highlight()
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

        public void Unhighlight()
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