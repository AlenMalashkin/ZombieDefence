using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MoneyCountDispalyer : MonoBehaviour
{
    [SerializeField] private Text moneyCountText;

    private Bank _bank;
    
    [Inject]
    private void Init(Bank bank)
    {
        _bank = bank;
    }

    private void Awake()
    {
        moneyCountText.text = _bank.Money + "";
    }

    private void OnEnable()
    {
        _bank.OnMoneyCountChangedEvent += UpdateMoneyCountText;
    }

    private void OnDisable()
    {
        _bank.OnMoneyCountChangedEvent -= UpdateMoneyCountText;
    }

    private void UpdateMoneyCountText(int currentMoneyAmount)
    {
        moneyCountText.text = currentMoneyAmount + "";
    }
}
