using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.PopUp
{
    public class PopUp : MonoBehaviour
    {
        [SerializeField] private float showDuration = 0.5f;
        [SerializeField] private float hideDuration = 0.5f;
        [SerializeField] private Ease showEase = Ease.OutQuint;
        [SerializeField] private Ease hideEase = Ease.InQuint;
        [SerializeField] private CanvasGroup canvasGroup;

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

    }
}