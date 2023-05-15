using UnityEngine;
using Zenject;

public class EnemyDie : State
{
    [SerializeField] private EnemyAnimator animator;
    [SerializeField] private AudioClip dieSound;
    
    private Collider[] _colliders;
    private int _moneyByKillEnemy;
    private Bank _bank;
    private Sound _sound;
    
    [Inject]
    private void Init(Bank bank, Sound sound)
    {
        _bank = bank;
        _sound = sound;
    }
    
    private void Die()
    {
        _sound.PlaySfx(dieSound);
        
        _bank.AddMoney(_moneyByKillEnemy);
           
        animator.PlayAnimation(false, false, true);

        foreach (Collider collider in _colliders) 
        {
            collider.enabled = false;
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void SetupDie(Collider[] colliders, int reward)
    {
        _colliders = colliders;
        _moneyByKillEnemy = reward;
    }
    
    public override void EnterState()
    {
        enabled = true;
        Die();
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        enabled = false;
    }
}
