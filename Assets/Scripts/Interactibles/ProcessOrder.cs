using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Interactibles
{
    public class ProcessOrder : MonoBehaviour, IInteractable
    {
        [SerializeField] private Text _orderText;

        [SerializeField] private Transform PlateSpawnPoint;
        [SerializeField] private GameObject _plate;
        [SerializeField] private ParticleSystem _sellOrderParticles;

        public void Interact()
        {
            if (_plate.transform.position != PlateSpawnPoint.position)
            {
                _orderText.text = "Cheese burger x1";
                _plate.transform.position = PlateSpawnPoint.position;
            }
            else
            {
                _sellOrderParticles.Play();
                _plate.transform.position = new Vector3(999, 999, 999);
            }
        }
    }
}