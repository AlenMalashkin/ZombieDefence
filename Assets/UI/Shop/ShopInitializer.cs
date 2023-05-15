using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShopInitializer : MonoBehaviour
{
    [SerializeField] private Item itemPrefab;
    [SerializeField] private Transform itemsContainer;

    private DiContainer _diContainer;
    private Dictionary<ItemType, ItemData> _itemDataMap;

    [Inject]
    private void Init(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }
    
    private void Awake()
    {
        _itemDataMap = new Dictionary<ItemType, ItemData>()
        {
            {ItemType.Damage, Resources.Load<ItemData>("Shop/DamageItemData")},
            {ItemType.Health, Resources.Load<ItemData>("Shop/HealthItemData")},
            {ItemType.FireRate, Resources.Load<ItemData>("Shop/FireRateItemData")}
        };
        
        foreach (ItemType type in (ItemType[]) Enum.GetValues(typeof(ItemType)))
        {
            var item = _diContainer.InstantiatePrefabForComponent<Item>(itemPrefab, itemsContainer);
            item.SetupItem(type, _itemDataMap[type]);
        }
    }
}
