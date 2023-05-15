using System;

public interface IDamagable
{
	public event Action<int> OnHealthChangedEvent;
	
	void TakeDamage(int amount);
	int GetMaxHealth();
}
