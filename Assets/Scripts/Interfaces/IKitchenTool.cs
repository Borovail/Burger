using Item;

public interface IKitchenTool
{
    bool HasCookedIngridient { get; }
    bool CanCookIngridient(Ingridient ingridient);
    void ReceiveIngridient(Ingridient ingridient);
    Ingridient GiveIngridient();
}
