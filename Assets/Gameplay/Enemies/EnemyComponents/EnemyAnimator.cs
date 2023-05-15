using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayAnimation(bool running, bool attacking, bool dying)
    {
        animator.SetBool("Running", running);
        animator.SetBool("Attacking", attacking);
        animator.SetBool("Dying", dying);
    }

    public void SetAnimationSpeed(float speed)
    {
        animator.speed = speed;
    }
}
