using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Interactibles
{
    public class ProcessOrder : MonoBehaviour, IInteractable
    {
        [SerializeField] private Text _orderText;
        [SerializeField] private GameObject _plate;
        [SerializeField] private ParticleSystem _sellOrderParticles;

        [SerializeField] private AudioClip _takeOrderSound;
        [SerializeField] private AudioClip _sellOrderSound;

        private AudioSource _audioSource;
        private bool _plateSpawned;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Interact()
        {
            if (!_plateSpawned)
            {
                _orderText.text = "Cheese burger x1";
                _plate.SetActive(true);
                _plate.transform.localPosition = new Vector3(0, 1f, 0);
                _audioSource.PlayOneShot(_takeOrderSound);
            }
            else
            {
                _orderText.text = "";
                _sellOrderParticles.Play();
                _plate.SetActive(false);
                _audioSource.PlayOneShot(_sellOrderSound);
            }

            _plate.SetActive(!_plateSpawned);
            _plateSpawned = !_plateSpawned;
        }
    }
}