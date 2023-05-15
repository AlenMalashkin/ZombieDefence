using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
	public event Action<int> OnHealthChangedEvent;

	[SerializeField] private ParticleSystem particleSystem;

	private int _maxHealth;
	private int _currentHealth;

	public void TakeDamage(int amount)
	{
		_currentHealth -= amount;
		particleSystem.Play();
		OnHealthChangedEvent?.Invoke(_currentHealth);
	}
	
	public int GetMaxHealth()
		=> _maxHealth;

	public void SetupHealth(int health)
	{
		_maxHealth = health;
		_currentHealth = _maxHealth;
	}
}
