using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerAnimationState currentState;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        currentState = PlayerAnimationState.Idle;
    }

    public void SetAnimationState(PlayerAnimationState state)
    {
        if (currentState == state) return;
        currentState = state;

        switch (state)
        {
            case PlayerAnimationState.Idle:
                animator.SetBool("Idle", true);
                animator.SetBool("Running", false);
                break;
            case PlayerAnimationState.Running:
                animator.SetBool("Idle", false);
                animator.SetBool("Running", true);
                break;
            case PlayerAnimationState.Death:
                animator.SetTrigger("Death");
                break;
            default:
                break;
        }
    }
}

public enum PlayerAnimationState
{
    Idle,
    Running,
    Death
}