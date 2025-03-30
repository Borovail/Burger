using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    private int money = 100;
    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = "Money: " + money;
    }
    
    public void RemoveMoney(int amount)
    {
        money -= amount;
        moneyText.text = "Money: " + money;
    }

    public bool HaveMoneyAmount(int amount)
    {
        return money >= amount;
    }
}
