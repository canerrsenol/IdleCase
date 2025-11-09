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
    private Transform playerTransform;
    private EnemyPool enemyPool;
    private GameManager gameManager;
    private bool isDead = false;
    private bool isAttacking = false;
    
    // Performans optimizasyonları
    private float updateInterval = 0.1f;
    private float nextUpdateTime = 0f;
    private float attackRangeSqr;
    private Vector3 cachedPlayerPosition;
    private WaitForSeconds deathDelay;

    void Awake()
    {
        enemyPool = EnemyPool.Instance;
        gameManager = GameManager.Instance;
        deathDelay = new WaitForSeconds(2f);
    }

    void OnEnable()
    {
        gameManager.OnGameStateChanged += OnGameStateChanged;

        if (enemyPool == null)
            enemyPool = EnemyPool.Instance;

        agent.enabled = true;
        enemyCollider.enabled = true;
        isDead = false;
        isAttacking = false;
        health = enemySettings.maxHealth;
        
        agent.speed = enemySettings.moveSpeed;
        agent.acceleration = enemySettings.acceleration;
        agent.angularSpeed = enemySettings.angularSpeed;
        
        attackRangeSqr = enemySettings.attackRange * enemySettings.attackRange;
        
        animationController.SetAnimationState(EnemyAnimationState.Running);
        
        nextUpdateTime = 0f;
    }

    void OnDisable()
    {
        gameManager.OnGameStateChanged -= OnGameStateChanged;
        StopAllCoroutines();
    }

    private void OnGameStateChanged(GameState gameState)
    {
        if (gameState != GameState.Started)
        {
            playerController = null;
            playerTransform = null;
            agent.enabled = false;

            if (enemyPool != null)
                enemyPool.SendToPool(this);
        }
    }

    public void Init(PlayerController target)
    {
        playerController = target;
        if (target != null)
        {
            playerTransform = target.transform;
            cachedPlayerPosition = playerTransform.position;
        }
    }

    void Update()
    {
        if (gameManager.GameState != GameState.Started || isDead) return;
        
        if (playerTransform == null) return;

        if (Time.time < nextUpdateTime) return;
        
        nextUpdateTime = Time.time + updateInterval;
        
        cachedPlayerPosition = playerTransform.position;

        if (!isAttacking)
        {
            float distanceSqr = (transform.position - cachedPlayerPosition).sqrMagnitude;
            
            if (distanceSqr <= attackRangeSqr)
            {
                isAttacking = true;
                agent.isStopped = true;
                agent.ResetPath();
                animationController.SetAnimationState(EnemyAnimationState.Attack);
            }
            else
            {
                if (agent.isStopped)
                {
                    agent.isStopped = false;
                }
                
                if (!agent.hasPath || !agent.pathPending)
                {
                    agent.SetDestination(cachedPlayerPosition);
                }
                
                animationController.SetAnimationState(EnemyAnimationState.Running);
            }
        }
    }

    public void OnAttackHit()
    {
        if (isDead) return;

        if (playerController != null)
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
        isAttacking = false;
        agent.isStopped = true;
        agent.ResetPath();
        agent.enabled = false;
        enemyCollider.enabled = false;
        animationController.SetAnimationState(EnemyAnimationState.Death);

        globalEvents.EnemyEvents.OnEnemyDeath?.Invoke();

        StartCoroutine(DieAndReturnToPool());
    }

    private System.Collections.IEnumerator DieAndReturnToPool()
    {
        yield return deathDelay;
        enemyPool.SendToPool(this);
    }
}