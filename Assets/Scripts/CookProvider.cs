using Item;
using KitchenTools;
using UnityEngine;

namespace DefaultNamespace
{
    public class CookProvider : MonoBehaviour
    {
        public static CookProvider Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ConvertItem(ToolType toolType, Ingridient item)
        {
            Debug.Log("Cooking started!");
        }
    }
}