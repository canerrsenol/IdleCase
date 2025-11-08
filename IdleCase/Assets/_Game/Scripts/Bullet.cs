using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletSettingsSO settings;
    private float lifetimeTimer;
    private BulletPool bulletPool;
    private Transform cachedTransform;
    private bool isActive;

    private void Awake()
    {
        bulletPool = BulletPool.Instance;
        cachedTransform = transform;
    }

    private void OnEnable()
    {
        lifetimeTimer = 0f;
        isActive = true;
    }

    private void Update()
    {
        if (!isActive) return;

        cachedTransform.Translate(cachedTransform.forward * settings.speed * Time.deltaTime, Space.World);

        lifetimeTimer += Time.deltaTime;
        if (lifetimeTimer >= settings.lifetime)
        {
            BackToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(settings.damage);
        }

        BackToPool();
    }

    private void BackToPool()
    {
        isActive = false;
        bulletPool.SendToPool(this);
    }
}
