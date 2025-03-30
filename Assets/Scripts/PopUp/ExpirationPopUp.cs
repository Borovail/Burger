using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.PopUp
{
    public class ExpirationPopUp : PopUp
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private Image iconImage;
        [SerializeField] private Image flavourIconImage;

        public void SetupExpirationPopUp(Sprite iconSprite, float similarity, Sprite flavourIcon = null)
        {
            iconImage.sprite = iconSprite;
            fillImage.fillAmount = similarity;
            if (flavourIcon != null)
            {
                flavourIconImage.sprite = flavourIcon;
            }
        }

        public void SetFlavourIcon(Sprite flavourIcon)
        {
            flavourIconImage.sprite = flavourIcon;
        }
        
        public void UpdateFillAmount(float similarity)
        {
            fillImage.fillAmount = similarity;
        }
    }
}