using DefaultNamespace;
using Item;
using UnityEngine;
using UnityEngine.Serialization;

namespace KitchenTools
{
    public class Mixer : KitchenTool
    {
        [SerializeField] private MixerAnimation animation;
        [SerializeField] private Timer localCookTimer;
        private Ingredient mainIngredient;
        private Ingredient flavourIngredient;
        
        protected override void Awake()
        {
            base.Awake();
            localCookTimer.OnTimerComplete += LocalCookTimerOnOnTimerComplete;
            animation.SetTotalDuration(cookDuration);
        }

        
        private void LocalCookTimerOnOnTimerComplete()
        {
            if (ingredientsToCook.Count == 0)
            {
                Debug.LogError("No item to cook");
                return;
            }

            if (ingredientsToCook.Count == 1)
            {
                Debug.LogError("Not enough ingredients to cook");
                return;
            }
            if(!SetupIngredients()) return;
            mainIngredient.AddFlavour(flavourIngredient.Type);
            ingredientsToCook.Remove(flavourIngredient);
            Destroy(flavourIngredient.gameObject);
            mainIngredient = null;
            flavourIngredient = null;
        }

        public override void Interact()
        {
            if (CanRunTool())
            {
                RunTool();
            }
        }

        private bool SetupIngredients()
        {
            Ingredient localMain = null;
            Ingredient localFlavour = null;
            foreach (Ingredient ingredient in ingredientsToCook)
            {
                if (!localMain && CookProvider.Instance.IsMainIngredient(ingredient.Type))
                {
                    localMain = ingredient;
                }
                if (!localFlavour && CookProvider.Instance.IsFlavour(ingredient.Type))
                {
                    localFlavour = ingredient;
                }
                if(localMain && localFlavour) break;
            }

            if (localMain && localFlavour)
            {
                mainIngredient = localMain;
                flavourIngredient = localFlavour;
                return true;
            }
            return false;
        }

        protected override void RunTool()
        {
            base.RunTool();
            animation.Animate();
            localCookTimer.StartTimer(cookDuration * 0.5f);
        }

        public override void Highlight()
        {
            if (CanRunTool())
            {
                Debug.Log(gameObject.name + " is highlighted");
                base.Highlight();
            }
        }
    }
}