using DefaultNamespace;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Interactibles
{
    public class ProcessOrder : MonoBehaviour, IInteractable
    {
        [SerializeField] private Text _orderText;
        [SerializeField] private GameObject _plate;

        private bool _plateSpawned;


        public void Interact()
        {
            if (!_plateSpawned)
            {
                _orderText.text = "Cheese burger x1";
                _plate.SetActive(true);
                _plate.transform.localPosition = new Vector3(0, 1f, 0);
                EffectManager.Instance.PlaySfx(EffectManager.Instance.TakeOrderAudio);
            }
            else
            {
                _orderText.text = "";
                _plate.SetActive(false);
                EffectManager.Instance.PlaySfx(EffectManager.Instance.SellOrderAudio)
                    .PlayParticles(EffectManager.Instance.SellOrderParticle,transform.position);
            }

            _plate.SetActive(!_plateSpawned);
            _plateSpawned = !_plateSpawned;
        }
    }
}