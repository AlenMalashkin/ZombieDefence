using UnityEngine;
using Zenject;

public class WeaponInstaller : MonoInstaller
{
    [SerializeField] private Transform weaponSpawnPoint;
    [SerializeField] private Transform projectileContainer;
    [SerializeField] private GameObject weaponPrefab;
    
    public override void InstallBindings()
    {
        WeaponUpgrades weaponUpgrades = new WeaponUpgrades();

        Container
            .Bind<WeaponUpgrades>().
            FromInstance(weaponUpgrades)
            .AsSingle();
        
        Weapon weapon =
            Container.InstantiatePrefabForComponent<Weapon>(weaponPrefab, weaponSpawnPoint.position, weaponPrefab.transform.rotation, null);

        weapon.SetupProjectilePool(projectileContainer);

        Container
            .Bind<Weapon>()
            .FromInstance(weapon)
            .AsSingle();
    }
}