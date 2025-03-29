public interface IKitchenTool
{
    bool CanUseItem(Item.Item item);
    void ReceiveItem(Item.Item item);
}
