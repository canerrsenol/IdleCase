using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletSettingsSO settings;
    private float lifetimeTimer;
    private BulletPool bulletPool;
    private BulletHitParticlePool hitParticlePool;
    private Transform cachedTransform;
    private bool isActive;

    private void Awake()
    {
        bulletPool = BulletPool.Instance;
        hitParticlePool = BulletHitParticlePool.Instance;
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

            var hitParticle = hitParticlePool.GetFromPool();
            hitParticle.transform.position = cachedTransform.position;
            hitParticle.transform.rotation = Quaternion.LookRotation(-cachedTransform.forward);
            hitParticle.gameObject.SetActive(true);
        }

        BackToPool();
    }

    private void BackToPool()
    {
        isActive = false;
        bulletPool.SendToPool(this);
    }
}
