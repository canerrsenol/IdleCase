using UnityEngine;

public class BulletHitParticle : MonoBehaviour
{
    private BulletHitParticlePool pool;

    private void Awake()
    {
        pool = BulletHitParticlePool.Instance;
    }

    void OnParticleSystemStopped()
    {
        pool.SendToPool(this);
    }
}
