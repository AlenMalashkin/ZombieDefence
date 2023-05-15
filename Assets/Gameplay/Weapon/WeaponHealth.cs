using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class WeaponHealth : MonoBehaviour, IDamagable
{
	public event Action<int> OnHealthChangedEvent;

	[SerializeField] private int health;

	private int _currentHealth;

	private CurrentAfterWaveState _currentAfterWaveState;
	private WeaponUpgrades _weaponUpgrades;

	[Inject]
	private void Init(CurrentAfterWaveState currentAfterWaveState, WeaponUpgrades weaponUpgrades)
	{
		_currentAfterWaveState = currentAfterWaveState;
		_weaponUpgrades = weaponUpgrades;
	}
	
	private void Awake()
	{
		health = _weaponUpgrades.Health;
		_currentHealth = health;
	}

	private void EndGame()
	{
		_currentAfterWaveState.State = AfterWaveState.Defeat;
		SceneManager.LoadScene("AfterWave");
	}

	public void TakeDamage(int amount)
	{
		_currentHealth -= amount;
		
		OnHealthChangedEvent?.Invoke(_currentHealth);
		
		if (_currentHealth <= 0)
			EndGame();
	}

	public int GetMaxHealth()
		=> health;
}
