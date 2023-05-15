using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private TriggerObserver triggerObserver;
    [SerializeField] private EnemyHealth enemyHealth;

    [Header("States")]
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] private EnemyDie enemyDie;

    [Header("Other")]
    [SerializeField] private Collider[] colliders;

    private Weapon _target;
    private Bank _bank;
    private EnemyStatSetuper _enemyStatSetuper;

    [Inject]
    private void Init(Weapon target, EnemyStatSetuper enemyStatSetuper)
    {
        _target = target;
        _enemyStatSetuper = enemyStatSetuper;
    }

    private void Awake()
    {
        enemyMovement.SetupMovement(_enemyStatSetuper.Speed, _target.transform.position);
        enemyAttack.SetupAttack(_enemyStatSetuper.Damage, _enemyStatSetuper.AttackRate);
        enemyDie.SetupDie(colliders, _enemyStatSetuper.Reward);
        enemyHealth.SetupHealth(_enemyStatSetuper.Health);
        
        stateMachine.SetState(enemyMovement);
    }

    private void OnEnable()
    {
        triggerObserver.OnTriggerEntered += TriggerEntered;
        triggerObserver.OnTriggerExited += TriggerExited;
        enemyHealth.OnHealthChangedEvent += CheckEnemyHealth;
    }

    private void OnDisable()
    {
        enemyHealth.OnHealthChangedEvent -= CheckEnemyHealth;
        triggerObserver.OnTriggerEntered -= TriggerEntered;
        triggerObserver.OnTriggerExited -= TriggerExited;
    }

    private void TriggerEntered(Collider other)
    {
        if (other.TryGetComponent(out Weapon weapon))
        {
            stateMachine.SetState(enemyAttack);
            enemyAttack.SetAttackTarget(weapon.GetComponent<IDamagable>());
        }
    }

    private void TriggerExited(Collider other)
    {
        if (other.TryGetComponent(out Weapon weapon))
        {
            enemyAttack.UnsetAttackTarget();
            stateMachine.SetState(enemyMovement);
        }
    }

    private void CheckEnemyHealth(int health)
    {
        if (health <= 0)
        {
            stateMachine.SetState(enemyDie);
        }
    }
}
