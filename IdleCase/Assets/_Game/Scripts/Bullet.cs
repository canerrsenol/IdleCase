using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(damage);
        }
    }
}
