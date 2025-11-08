using UnityEngine;

public class EnemyAnimationCallback : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;

    public void AttackHit()
    {
        enemyController.OnAttackHit();
    }
}
