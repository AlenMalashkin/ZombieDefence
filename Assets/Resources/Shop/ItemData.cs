using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Items", order = 0)]
public class ItemData : ScriptableObject
{
    [SerializeField] private bool hasUpgradeLimit;
    [SerializeField] private Sprite sprite;
    [SerializeField] private string itemName;
    [SerializeField] private int cost;
    [SerializeField] private int upgradeLimit;
    [SerializeField] private string savePath;
    
    public bool HasUpgradeLimit => hasUpgradeLimit;
    public int UpgradeLimit => upgradeLimit;
    public Sprite Sprite => sprite;
    public string ItemName => itemName;
    public int Cost => cost;
    public string SavePath => savePath;
}
