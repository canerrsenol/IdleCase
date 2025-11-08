using UnityEngine;

public class LevelData : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("Level süresi (saniye cinsinden). Örn: 180 = 3 dakika")]
    public float totalSeconds = 180f;

    [Header("Wave Settings")]
    [Tooltip("İlk dalgadaki düşman sayısı.")]
    public int baseEnemyCount = 5;

    [Tooltip("Her dalgada düşman sayısına eklenecek miktar.")]
    public int enemyCountIncrement = 2;

    [Tooltip("Dalgalar arası bekleme süresi (saniye).")]
    public float timeBetweenWaves = 20f;

    [Tooltip("Spawn yarıçapı (oyuncunun etrafında).")]
    public float spawnRadius = 8f;

    [Tooltip("Aynı wave içindeki düşman spawn aralığı (saniye).")]
    public float spawnInterval = 0.3f;
}
