using DG.Tweening;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

namespace KitchenTools
{
    public class MixerAnimation : MonoBehaviour
    {
        [SerializeField] private Transform cap;
        [SerializeField] private Transform capStartPoint;
        [SerializeField] private Transform capEndPoint;
        [SerializeField] private float capDuration = 0.5f;
        [SerializeField] private float cupJumpPower = 0.5f;
        [SerializeField] private float height = 0.5f;
        [SerializeField] private float heightDuration = 0.1f;
        [SerializeField] private Vector3 shakeStrength;
        private float totalDuration = 5f;
        private void Start()
        {
            cap.transform.position = capStartPoint.position;
            cap.transform.rotation = capStartPoint.rotation;
        }
        
        public void SetTotalDuration(float duration)
        {
            totalDuration = duration;
        }

        public void Animate()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(cap.DOJump(capEndPoint.position,cupJumpPower, 1 , capDuration));
            sequence.Join(cap.DORotate(capEndPoint.eulerAngles, capDuration));
            sequence.Append(transform.DOLocalMoveY(transform.localPosition.y + height, heightDuration));
            float shakeDuration = Mathf.Max(2f, totalDuration - 2 * (capDuration + heightDuration));
            sequence.Append(transform.DOShakePosition(shakeDuration, shakeStrength * Time.deltaTime, vibrato:5));
            sequence.Append(transform.DOLocalMoveY(transform.localPosition.y, heightDuration));
            sequence.Append(cap.DOJump(capStartPoint.position, cupJumpPower, 1 , capDuration));
            sequence.Join(cap.DORotate(capStartPoint.eulerAngles, capDuration));
        }
    }
}