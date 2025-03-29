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
        [SerializeField] private Image iconImage;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Sprite startIcon;
        [SerializeField] private Sprite finishIcon;
        
        private bool isVisible;
        public bool IsVisible => isVisible;
        
        private void Awake()
        {
            HideInstant();
        }
        
        public void Show()
        {
            isVisible = true;
            canvasGroup.DOFade(1f, showDuration).SetEase(showEase);
        }

        public void Hide()
        {
            isVisible = false;
            canvasGroup.DOFade(0f, hideDuration).SetEase(hideEase);
        }

        private void HideInstant()
        {
            isVisible = false;
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
            iconImage.sprite = startIcon;
            Show();
        }

        protected override void FinishTimer()
        {
            base.FinishTimer();
            iconImage.sprite = finishIcon;
        }
    }
}