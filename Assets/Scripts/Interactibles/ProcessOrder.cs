using System;
using System.Collections.Generic;
using DefaultNamespace;
using Interfaces;
using Item;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Random = UnityEngine.Random;

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

        public event Action<Receipt> OnOrderGet;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            trigger.OnEnter += TriggerOnOnEnter;
            trigger.OnExit += TriggerOnOnExit;
        }

        private void TriggerOnOnEnter(Collider other)
        {
            if (other.CompareTag(Tags.Dish))
            {
                _dish = other.GetComponent<Dish>();
            }
        }
        
        private void TriggerOnOnExit(Collider other)
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
            }
            _receipt = GetRandomReceipt();
            _orderText.text = _receipt.Title;
            _descriptionText.text = _receipt.Description;
            _audioSource.PlayOneShot(_takeOrderSound);
            
            OnOrderGet?.Invoke(_receipt);
        }

        private void GiveDish()
        {
            _orderText.text = "";
            _descriptionText.text = "";
            _sellOrderParticles.Play();
            Destroy(_tray.gameObject);
            Destroy(_dish.gameObject);
            _tray = null;
            _dish = null;
            _receipt = null;
            _audioSource.PlayOneShot(_sellOrderSound);
        }
    }
}