using System;
using System.Collections;
using DefaultNamespace;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Interactions
{
    [RequireComponent(typeof(Highlightable))]
    public class Knife : MonoBehaviour, IPickable
    {
        private Rigidbody _rigidbody;
        private Collider _collider;

        // Расстояние, на которое нож "штрыкается" вперёд
        public float thrustDistance = 0.5f;
        // Общее время анимации штрыка (вперёд + возврат)
        public float thrustDuration = 0.2f;

        public event Action OnPickedUp;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public Rigidbody GetRigidbody()
        {
            return _rigidbody;
        }

        public bool CanPickUp()
        {
            return true;
        }

        public void PickUp()
        {
        }

        public void Drop()
        {
        }

        // Метод, который выполняется при "штрыке" ножом
        public void Stab()
        {
            //// Определяем начало луча: используем позицию ножа (если нож прикреплён к руке, позиция будет в мировых координатах)
            //Vector3 origin = transform.position;
            //// Направление удара — вперед относительно ножа
            //Vector3 direction = transform.up;

            //// Для наглядности можно отобразить луч в редакторе
            //Debug.DrawRay(origin, direction * thrustDistance, Color.red, 1f);

            //// Выполняем проверку с помощью Raycast
            //if (Physics.Raycast(origin, direction, out RaycastHit hit, thrustDistance))
            //{
            //    //if(hit.collider.TryGetComponent(out Rat rat))
            //    Debug.Log("Knife thrust hit: " + hit.collider.gameObject.name);
            //    // Если объект имеет компонент, реализующий, например, IDamageable — наносим урон
            //    //var damageable = hit.collider.GetComponent<IDamageable>();
            //    //if (damageable != null)
            //    //{
            //    //    damageable.TakeDamage(thrustDamage);
            //    //}
            //    //// Можно добавить и другие виды взаимодействия, если нужно
            //}

            if (!_collider.isTrigger)
            {
                EffectManager.Instance.PlaySfx(EffectManager.Instance.KnifeStubAudio);
                StartCoroutine(ThrustCoroutine());
            }
        }

        private IEnumerator ThrustCoroutine()
        {
            _collider.isTrigger = true;

            // Сохраняем исходную локальную позицию ножа
            Vector3 originalLocalPos = transform.localPosition;
            // Целевая локальная позиция – смещение вперед на thrustDistance (в локальных координатах, т.е. по оси Z)
            Vector3 targetLocalPos = originalLocalPos + Vector3.forward * thrustDistance;
            float halfDuration = thrustDuration / 2f;
            float elapsed = 0f;

            // Первая половина: смещение ножа вперёд
            while (elapsed < halfDuration)
            {
                transform.localPosition = Vector3.Lerp(originalLocalPos, targetLocalPos, elapsed / halfDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = targetLocalPos;

            // Вторая половина: возврат в исходное положение
            elapsed = 0f;
            while (elapsed < halfDuration)
            {
                transform.localPosition = Vector3.Lerp(targetLocalPos, originalLocalPos, elapsed / halfDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = originalLocalPos;

            _collider.isTrigger = false;

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Rat rat))
            {
                rat.TakeHit();
            }
        }
    }
}
