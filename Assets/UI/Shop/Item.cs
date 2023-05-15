using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Item : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Text itemName;
    [SerializeField] private Text costText;
    [SerializeField] private Text levelText;

    private int _cost;
    private ItemType _type;
    private Bank _bank;
    private ItemData _itemData;

    [Inject]
    private void Inti(Bank bank)
    {
        _bank = bank;
    }

    private void OnEnable()
    {
        buyButton.onClick.AddListener(UpgradeItem);
    }

    private void OnDisable()
    {
        buyButton.onClick.RemoveListener(UpgradeItem);
    }

    private void UpgradeItem()
    {
        if (_bank.SpendMoney(_cost))
        {
            var level = PlayerPrefs.GetInt(_itemData.SavePath, 1);
            level++;
            PlayerPrefs.SetInt(_itemData.SavePath, level);
            
            UpdateUI(level, _itemData.Cost);
        }
    }

    private void UpdateUI(int level, int cost)
    {
        _cost = cost * level;
        costText.text = _cost + "";
        levelText.text = "Уровень: " + level;
        CheckUpgradeLimit(level);
    }

    private void CheckUpgradeLimit(int currentLevel)
    {
        if (_itemData.HasUpgradeLimit && _itemData.UpgradeLimit == currentLevel)
        {
            buyButton.interactable = false;
            costText.text = "Макс.";
        }
    }

    public void SetupItem(ItemType type, ItemData itemData)
    {
        _type = type;
        _itemData = itemData;

        itemName.text = _itemData.ItemName;
        itemIcon.sprite = _itemData.Sprite;

        var level = PlayerPrefs.GetInt(_itemData.SavePath, 1);
        
        UpdateUI(level, _itemData.Cost);
    }
}
