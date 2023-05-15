using UnityEngine;
using Zenject;

public class EnemyAttack : State
{
    [SerializeField] private EnemyAnimator animator;
    [SerializeField] private AudioClip attackSound;

    private Sound _sound;
    private int _updatedDamage;
    private float _updatedAttackRate;
    private IDamagable _attackTarget;

    [Inject]
    private void Init(Sound sound)
    {
        _sound = sound;
    }
    
    private void Attack()
    {
        if (_attackTarget != null)
        {
            _attackTarget.TakeDamage(_updatedDamage);
            _sound.PlaySfx(attackSound);
        }
    }

    public void SetAttackTarget(IDamagable attackTarget)
    {
        _attackTarget = attackTarget;
    }

    public void UnsetAttackTarget()
    {
        _attackTarget = null;
    }

    public void SetupAttack(int updatedDamage, float updatedAttackRate)
    {
        _updatedDamage = updatedDamage;
        _updatedAttackRate = updatedAttackRate;
    }

    public override void EnterState()
    {
        enabled = true;
        animator.PlayAnimation(false, true, false);
        animator.SetAnimationSpeed(_updatedAttackRate);
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        animator.SetAnimationSpeed(1);
        enabled = false;
    }
}
