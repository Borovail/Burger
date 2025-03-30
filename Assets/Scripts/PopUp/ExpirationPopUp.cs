using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.PopUp
{
    public class ExpirationPopUp : PopUp
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private Image iconImage;

        public void SetupExpirationPopUp(Sprite iconSprite, float similarity)
        {
            iconImage.sprite = iconSprite;
            fillImage.fillAmount = similarity;
        }

        public void UpdateFillAmount(float similarity)
        {
            fillImage.fillAmount = similarity;
        }
    }
}