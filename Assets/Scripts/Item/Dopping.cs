using Interfaces;
using UnityEngine;

namespace Item
{
    public class Dopping : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform ingredientToSpawn;
        [SerializeField] private Transform spawnPoint;
        public void Interact()
        {
            Instantiate(ingredientToSpawn, spawnPoint.position, Quaternion.identity);
        }
    }
}