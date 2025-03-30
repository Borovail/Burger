using System;
using DefaultNamespace;
using Interfaces;
using Item;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Interactibles
{
    public class ProcessOrder : MonoBehaviour, IInteractable
    {
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
                _orderText.text = _receipt.Title;
                _orderText.text += "\n Base cost: " + _receipt.BaseCost;
                float expectedCost = (int)(_dish.CalculateSimilarity() * _receipt.BaseCost);
                _orderText.text += "\n Evaluated cost: " + expectedCost;
                Debug.Log(_receipt.BaseCost);
            }
        }
        
        private void TriggerOnOnExit(Collider other)
        {
            if (other.CompareTag(Tags.Dish))
            {
                Dish dish = other.GetComponent<Dish>();
                if (_dish == dish)
                {
                    _orderText.text = _receipt.Title;
                    _orderText.text += "\n Base cost: " + _receipt.BaseCost;
                    _dish = null;
                }
            }
        }
        
        public void Interact()
        {
            if (_receipt.Equals(default(Receipt)))
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
            _receipt = CookProvider.Instance.GetNextReceipt();
            _orderText.text = _receipt.Title;
            _orderText.text += "\n Base cost: " + _receipt.BaseCost;
            _descriptionText.text = _receipt.Description;
            _audioSource.PlayOneShot(_takeOrderSound);
            
            OnOrderGet?.Invoke(_receipt);
        }

        private void GiveDish()
        {
            _orderText.text = "";
            _descriptionText.text = "";
            _sellOrderParticles.Play();
            Player.Instance.AddMoney((int)(_dish.CalculateSimilarity() * _receipt.BaseCost));
            Destroy(_tray.gameObject);
            Destroy(_dish.gameObject);
            _tray = null;
            _dish = null;
            _receipt = default;
            _audioSource.PlayOneShot(_sellOrderSound);
        }
    }
}