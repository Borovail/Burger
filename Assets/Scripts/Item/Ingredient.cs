using System;
using System.Collections;
using Assets.Scripts.Interactions;
using DefaultNamespace;
using DefaultNamespace.PopUp;
using KitchenTools;
using UnityEngine;

namespace Item
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ingredient : Highlightable, IPickable
    {
        [SerializeField] private IngredientType type;
        [SerializeField]  private float similarityPercentage = 1f;
        
        [SerializeField] private IngredientType addedFlavour = IngredientType.Null;
        [SerializeField] private ExpirationPopUp ui;
        [SerializeField] private bool isFlavour;
        [SerializeField] private bool isCooked;
        private ToolType cookedTool = ToolType.Null;
        
        private float secondsToDecreaseSimilarity = 10f;
        private float decreaseValue = 0.03f;
        
        private float height;
        private bool canBePickedUp = true;
        private MeshFilter filter;
        private Rigidbody _rigidbody;
        private Coroutine decreaseSimilarityRoutine;
        
        public IngredientType AddedFlavour => addedFlavour;
        public float Height => height;
        public IngredientType Type => type;
        public float SimilarityPercentage => similarityPercentage;
        public bool IsCooked => isCooked;
        public ToolType CookedTool => cookedTool;

        protected override void Awake()
        {
            base.Awake();
            height = GetComponent<Collider>().bounds.size.y;
            filter = GetComponent<MeshFilter>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {            
            ui.SetupExpirationPopUp(CookProvider.Instance.IngredientsData.GetItemByType(type).Value.Icon, similarityPercentage);
            if (isFlavour)
            {
                similarityPercentage = 1;
                ui.UpdateFillAmount(similarityPercentage);
                return;
            }
            decreaseSimilarityRoutine = StartCoroutine(Co_DecreaseSimilarity());
        }

        private void OnDestroy()
        {
            if (decreaseSimilarityRoutine != null)
            {
                StopCoroutine(decreaseSimilarityRoutine);
            }
        }
        
        public void ChangeIngredient(float similarityPercent)
        {
            addedFlavour = IngredientType.Null;
            similarityPercentage = Mathf.Clamp01(similarityPercentage + similarityPercent);
            ui.UpdateFillAmount(similarityPercentage);
        }
        
        public void Cook(ToolType toolType)
        {
            isCooked = true;
            cookedTool = toolType;
            ui.SetCookToolIcon(toolType);
            IngredientData? itemData = CookProvider.Instance.IngredientsData.GetItemByType(type);
            if (itemData != null && itemData.Value.CookedMesh && itemData.Value.CookedMaterials != null)
            {
                filter.mesh = itemData.Value.CookedMesh;
                _renderer.materials = itemData.Value.CookedMaterials;
            }
        }

        public event Action OnPickedUp;

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
            return canBePickedUp;
        }

        public void PickUp()
        {
            OnPickedUp?.Invoke();
        }

        public void Drop()
        {
        }

        public void EnablePickUp()
        {
            canBePickedUp = true;
        }

        public void DisablePickUp()
        {
            canBePickedUp = false;
        }

        public override void Highlight()
        {
            base.Highlight();
            ui.Show();
        }

        public override void Unhighlight()
        {
            base.Unhighlight();
            ui.Hide();
        }

        private IEnumerator Co_DecreaseSimilarity()
        {
            while (true)
            {
                yield return new WaitForSeconds(secondsToDecreaseSimilarity);
                similarityPercentage -= decreaseValue;
                ui.UpdateFillAmount(similarityPercentage);
            }
        }

        public void AddFlavour(IngredientType flavour)
        {
            addedFlavour = flavour;
            ui.SetFlavourIcon(CookProvider.Instance.IngredientsData.GetItemByType(flavour).Value.Icon);
        }
    }
}