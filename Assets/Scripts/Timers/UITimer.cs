using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Timers
{
    public class UITimer : Timer
    {
        [SerializeField] private float showDuration = 0.5f;
        [SerializeField] private float hideDuration = 0.5f;
        [SerializeField] private Ease showEase = Ease.OutQuint;
        [SerializeField] private Ease hideEase = Ease.InQuint;
        [SerializeField] private Image fillImage;
        [SerializeField] private CanvasGroup canvasGroup;

        private void Awake()
        {
            HideInstant();
        }
        
        private void Show()
        {
            canvasGroup.DOFade(1f, showDuration).SetEase(showEase);
        }

        private void Hide()
        {
            canvasGroup.DOFade(0f, hideDuration).SetEase(hideEase);
        }

        private void HideInstant()
        {
            canvasGroup.alpha = 0;
        }

        protected override void UpdateTimer()
        {
            base.UpdateTimer();
            fillImage.fillAmount -= Time.deltaTime / duration;
        }
        
        public override void StartTimer(float newDuration = -1f)
        {
            base.StartTimer(newDuration);
            fillImage.fillAmount = 1f;
            Show();
        }

        protected override void FinishTimer()
        {
            base.FinishTimer();
            Hide();
        }
    }
}