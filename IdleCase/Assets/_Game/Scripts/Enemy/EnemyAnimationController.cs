using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private EnemyAnimationState currentState;

    public void SetAnimationState(EnemyAnimationState state)
    {
        if (currentState == state) return;

        currentState = state;
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
