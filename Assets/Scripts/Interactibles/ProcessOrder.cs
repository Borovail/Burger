using System.Collections.Generic;
using DefaultNamespace;
using Interfaces;
using Item;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Interactibles
{
    public class ProcessOrder : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<Receipt> receipts;
        [SerializeField] private Text _orderText;
        [SerializeField] private Text _descriptionText;
        [SerializeField] private Transform trayPrefab;
        [SerializeField] private Transform trayParent;
        [SerializeField] private ParticleSystem _sellOrderParticles;
        [SerializeField] private TriggerProvider trigger;
        
        [SerializeField] private AudioClip _takeOrderSound;
        [SerializeField] private AudioClip _sellOrderSound;

        private Dish _dish;
        private Transform _tray;
        private AudioSource _audioSource;

        private bool HaveDish => _dish != null;
        private Receipt _receipt;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            trigger.OnEnter += TriggerOnOnEnter;
            trigger.OnExit += TriggerOnOnExit;
            GetOrder();
        }

        private void TriggerOnOnExit(Collider other)
        {
            if (other.CompareTag(Tags.Dish))
            {
                _dish = other.GetComponent<Dish>();
            }
        }

        private void TriggerOnOnEnter(Collider other)
        {
            if (other.CompareTag(Tags.Dish))
            {
                Dish dish = other.GetComponent<Dish>();
                if (_dish == dish)
                {
                    _dish = null;
                }
            }
        }

        private Receipt GetRandomReceipt()
        {
            int randomIndex = Random.Range(0, receipts.Count);
            return receipts[randomIndex];
        }
        
        public void Interact()
        {
            if (!_receipt)
            {
                GetOrder();
            }
            else if(HaveDish)
            {
                GiveDish();
            }
        }

        private void GetOrder()
        {
            if (_tray == null)
            {
                _tray = Instantiate(trayPrefab, trayParent);
                _tray.localPosition = new Vector3(0, 1f, 0);
            }
            _receipt = GetRandomReceipt();
            _orderText.text = _receipt.Title;
            _descriptionText.text = _receipt.Description;
            _audioSource.PlayOneShot(_takeOrderSound);
        }

        private void GiveDish()
        {
            _orderText.text = "";
            _sellOrderParticles.Play();
            Destroy(_tray.gameObject);
            Destroy(_dish.gameObject);
            _tray = null;
            _dish = null;
            _audioSource.PlayOneShot(_sellOrderSound);
        }
    }
}