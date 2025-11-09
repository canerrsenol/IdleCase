using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemySettingsSO enemySettings;
    [SerializeField] private GlobalEventsSO globalEvents;
    [SerializeField] private float health = 100f;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Collider enemyCollider;
    [SerializeField] private EnemyAnimationController animationController;
    private PlayerController playerController;
    private EnemyPool enemyPool;
    private GameManager gameManager;
    private bool isDead = false;
    private bool isAttacking = false;

    void Awake()
    {
        enemyPool = EnemyPool.Instance;
        gameManager = GameManager.Instance;
    }

    void OnEnable()
    {
        gameManager.OnGameStateChanged += OnGameStateChanged;

        if (enemyPool == null)
            enemyPool = EnemyPool.Instance;

        agent.enabled = true;
        enemyCollider.enabled = true;
        isDead = false;
        health = enemySettings.maxHealth;
        animationController.SetAnimationState(EnemyAnimationState.Running);

        agent.speed = enemySettings.moveSpeed;
        agent.acceleration = enemySettings.acceleration;
        health = enemySettings.maxHealth;
        agent.angularSpeed = enemySettings.angularSpeed;
    }

    void OnDisable()
    {
        gameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState gameState)
    {
        if (gameState != GameState.Started)
        {
            playerController = null;
            agent.enabled = false;

            if (enemyPool != null)
                enemyPool.SendToPool(this);
        }
    }

    public void Init(PlayerController target)
    {
        playerController = target;
    }

    void Update()
    {
        if (gameManager.GameState != GameState.Started) return;

        if (playerController != null && !isDead && !isAttacking)
        {
            float distance = Vector3.Distance(transform.position, playerController.transform.position);
            if (distance <= enemySettings.attackRange)
            {
                isAttacking = true;
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

    public void OnAttackHit()
    {
        if (isDead) return;

        if (playerController != null && !isDead)
        {
            playerController.GetComponent<IDamageable>()?.TakeDamage(enemySettings.attackDamage);
        }
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        agent.isStopped = true;
        agent.enabled = false;
        enemyCollider.enabled = false;
        animationController.SetAnimationState(EnemyAnimationState.Death);

        globalEvents.EnemyEvents.OnEnemyDeath?.Invoke();

        StartCoroutine(DieAndReturnToPool());
    }

    private System.Collections.IEnumerator DieAndReturnToPool()
    {
        yield return new WaitForSeconds(2f);
        enemyPool.SendToPool(this);
    }
}
