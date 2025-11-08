using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetAnimationState(EnemyAnimationState state)
    {
        switch (state)
        {
            case EnemyAnimationState.Running:
                animator.SetTrigger("Running");
                break;
            case EnemyAnimationState.Attack:
                animator.SetTrigger("Attack");
                break;
            case EnemyAnimationState.Death:
                animator.SetTrigger("Death");
                break;
            default:
                break;
        }
    }
}

public enum EnemyAnimationState
{
    Running,
    Attack,
    Death
}
