using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Dish : MonoBehaviour
    {
        [SerializeField] private List<Ingridient> ingridients;

        public void AddIngridient(Ingridient ingridient)
        {
            ingridients.Add(ingridient);
        }

        public void RemoveIngridient(Ingridient ingridient)
        {
            ingridients.Remove(ingridient);
        }
    }
}