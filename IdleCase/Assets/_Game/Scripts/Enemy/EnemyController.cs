using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemySettingsSO enemySettings;
    private NavMeshAgent agent;
    private EnemyAnimationController animationController;
    private PlayerController playerController;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animationController = GetComponent<EnemyAnimationController>();
    }

    void Start()
    {
        agent.speed = enemySettings.moveSpeed;
        animationController.SetAnimationState(EnemyAnimationState.Running);
    }

    public void SetTarget(PlayerController target)
    {
        playerController = target;
    }

    void Update()
    {
        if (playerController != null)
        {
            float distance = Vector3.Distance(transform.position, playerController.transform.position);
            if (distance <= enemySettings.attackRange)
            {
                agent.isStopped = true;
                animationController.SetAnimationState(EnemyAnimationState.Attack);
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(playerController.transform.position);
                animationController.SetAnimationState(EnemyAnimationState.Running);
            }
        }
    }
}
