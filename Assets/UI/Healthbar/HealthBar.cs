using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private Image healthBar;
	[SerializeField] private GameObject damagableObject;
	
	private IDamagable _damagable;

	private void Awake()
	{
		if (damagableObject.TryGetComponent(out IDamagable damagable))
		{
			_damagable = damagable;
		}
		else
		{
			Debug.LogError($"There is no IDamagable on object {damagableObject.name}");
		}
	}

	private void OnEnable()
	{
		_damagable.OnHealthChangedEvent += OnHealthChanged;
	}

	private void OnDisable()
	{
		_damagable.OnHealthChangedEvent -= OnHealthChanged;
	}

	private void OnHealthChanged(int health)
	{
		healthBar.fillAmount = (float) health / _damagable.GetMaxHealth();
	}
}
