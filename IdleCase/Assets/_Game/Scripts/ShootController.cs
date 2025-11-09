using UnityEngine;

public class ShootController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private ShootSettingsSO shootSettings;
    [SerializeField] private ParticleSystem shootEffect;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform characterVisualTransform;
    [SerializeField] private float rotationSpeed = 8f;

    private BulletPool bulletPool;
    private EnemyController closestEnemy;
    private float shootTimer;
    private bool canShoot = true;

    private void Awake()
    {
        bulletPool = BulletPool.Instance;
    }

    private void FixedUpdate()
    {
        if (!canShoot) return;

        FindClosestEnemy();
    }

    public void SetShootControllerState(bool state)
    {
        canShoot = state;
    }

    public bool HasTarget()
    {
        return closestEnemy != null;
    }

    private void Update()
    {
        if (!canShoot) return;
        if (closestEnemy != null)
        {
            Vector3 direction = (closestEnemy.transform.position - characterVisualTransform.position).normalized;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction);
                characterVisualTransform.rotation = Quaternion.Slerp(
                    characterVisualTransform.rotation,
                    targetRot,
                    rotationSpeed * Time.deltaTime
                );
            }

            float angle = Vector3.Angle(characterVisualTransform.forward, direction);
            if (angle < 10f)
            {
                TryShoot();
            }
        }
    }

    private void TryShoot()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer < shootSettings.shootCooldown) return;

        shootTimer = 0f;

        Bullet bullet = bulletPool.GetFromPool();
        bullet.transform.position = shootPoint.position;

        var lookPosition = closestEnemy.transform.position;
        lookPosition.y = bullet.transform.position.y;

        bullet.transform.LookAt(lookPosition);
        bullet.gameObject.SetActive(true);

        shootEffect.Play();
    }

    private void FindClosestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, shootSettings.checkAreaRadius, enemyLayerMask);
        float minDistance = Mathf.Infinity;
        closestEnemy = null;

        foreach (var hit in hits)
        {
            EnemyController enemy = hit.GetComponent<EnemyController>();
            if (enemy == null) continue;

            float distance = Vector3.SqrMagnitude(enemy.transform.position - transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootSettings.checkAreaRadius);
    }
}
