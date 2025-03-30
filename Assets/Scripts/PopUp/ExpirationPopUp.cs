using KitchenTools;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.PopUp
{
    public class ExpirationPopUp : PopUp
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private Image iconImage;
        [SerializeField] private Image flavourIconImage;
        [SerializeField] private Image cookToolIconImage;
        [SerializeField] private Sprite panSprite;
        [SerializeField] private Sprite ovenSprite;

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

        public void SetCookToolIcon(ToolType cookType)
        {
            if (cookType == ToolType.Oven)
            {
                cookToolIconImage.sprite = ovenSprite;
            } 
            else if (cookType == ToolType.Pan)
            {
                cookToolIconImage.sprite = panSprite;
            }
        }
    }
}