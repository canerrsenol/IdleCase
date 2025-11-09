using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;
    [SerializeField] private MovementController movementController;
    [SerializeField] private ShootController shootController;
    [SerializeField] private PlayerAnimationController animationController;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    void OnEnable()
    {
        gameManager.OnGameStateChanged += OnGameStateChanged;
    }

    void OnDisable()
    {
        if (gameManager != null) gameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.Win)
        {
            movementController.SetMovementControllerState(false);
            shootController.SetShootControllerState(false);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameManager.ChangeGameState(GameState.Lose);
        shootController.SetShootControllerState(false);
        movementController.SetMovementControllerState(false);
        animationController.SetAnimationState(PlayerAnimationState.Death);
    }
}
