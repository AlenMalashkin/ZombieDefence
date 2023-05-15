using System;
using UnityEngine;

public class EnemyStatSetuper
{
    private readonly int _currentWave;
    private readonly int _defaultDamage;
    private readonly int _defaultHealth;
    private readonly float _defaultSpeed;
    private readonly float _defaultAttackRate;

    public int Damage
    {
        get
        {
            var updatedDamage = _defaultDamage;
            
            for (int i = 1; i <= _currentWave; i++)
            {
                if (i % 3 == 0)
                    updatedDamage += 1;
            }

            return updatedDamage;
        }
    }

    public int Health
    {
        get
        {
            var updatedHealth = _defaultHealth;
            
            for (int i = 1; i <= _currentWave; i++)
            {
                updatedHealth += 1;
            }

            return updatedHealth;
        }
    }

    public float Speed
    {
        get
        {
            var updatedSpeed = _defaultSpeed;
            
            for (int i = 1; i <= _currentWave; i++)
            {
                if (i % 3 == 0 && updatedSpeed < 40)
                    updatedSpeed += 1;
            }

            return updatedSpeed;
        }
    }

    public float AttackRate
    {
        get
        {
            var defaultAttackRate = _defaultAttackRate;
            
            for (int i = 1; i <= _currentWave; i++)
            {
                if (i % 10 == 0 && defaultAttackRate > 0.1f)
                    defaultAttackRate += 0.1f;
            }

            return defaultAttackRate;
        }
    }

    public int Reward
    {
        get
        {
            var reward = 30;
            
            for (int i = 1; i < _currentWave; i++)
            {
                if (i % 10 == 0)
                    reward *= 2;
            }
            
            return reward;
        }
    }

    public EnemyStatSetuper(int defaultDamage, int defaultHealth, float defaultSpeed, float defaultAttackRate)
    {
        _currentWave = PlayerPrefs.GetInt("CurrentWave", 1);

        _defaultDamage = defaultDamage;
        _defaultHealth = defaultHealth;
        _defaultSpeed = defaultSpeed;
        _defaultAttackRate = defaultAttackRate;
    }
}
