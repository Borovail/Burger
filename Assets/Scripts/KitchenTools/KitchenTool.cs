using UnityEngine;

namespace KitchenTools
{
    public abstract class KitchenTool : MonoBehaviour, IKitchenTool
    {
        public abstract void ReceiveItem(Item item);
        public abstract void Interact();
        
    }
}