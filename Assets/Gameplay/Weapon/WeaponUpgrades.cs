using UnityEngine;

public class WeaponUpgrades
{
    public int DamageUpgradeLevel { get; private set; }
    public int HealthUpgradeLevel { get; private set; }
    public int FireRateUpgradeLevel { get; private set; }

    public int Damage
    {
        get
        {
            var calculatedDamage = 1;

            for (int i = 1; i < DamageUpgradeLevel; i++)
            {
                calculatedDamage += 1;
            }
            
            return calculatedDamage;
        }
    }

    public int Health
    {
        get
        {
            var calculatedHealth = 100;

            for (int i = 1; i < HealthUpgradeLevel; i++)
            {
                calculatedHealth += 100;
            }
            
            return calculatedHealth;
        }
    }

    public float FireRate
    {
        get
        {
            var calculatedFireRate = 1f;

            for (int i = 1; i < FireRateUpgradeLevel; i++)
            {
                if (calculatedFireRate > 0.1)
                    calculatedFireRate -= 0.1f;
            }

            return calculatedFireRate;
        }
    }
    
    public WeaponUpgrades()
    {
        DamageUpgradeLevel = PlayerPrefs.GetInt("WeaponDamageLevel", 1);
        HealthUpgradeLevel = PlayerPrefs.GetInt("WeaponHealthLevel", 1);
        FireRateUpgradeLevel = PlayerPrefs.GetInt("WeaponFireRateLevel", 1);
    }
}
