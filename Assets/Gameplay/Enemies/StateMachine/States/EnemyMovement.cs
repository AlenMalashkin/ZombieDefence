using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : State
{
	[SerializeField] private EnemyAnimator animator;
	[SerializeField] private NavMeshAgent agent;

	private Vector3 _target;

	private void Move()
	{
		agent.SetDestination(_target);
	}

	public void SetupMovement(float speed, Vector3 target)
	{
		agent.speed = speed;
		_target = target;
	}

	public override void EnterState()
	{
		enabled = true;
		agent.enabled = true;
		animator.PlayAnimation(true, false, false);
	}

	public override void UpdateState()
	{
		Move();
	}

	public override void ExitState()
	{
		agent.enabled = false;
		enabled = false;
	}
}
