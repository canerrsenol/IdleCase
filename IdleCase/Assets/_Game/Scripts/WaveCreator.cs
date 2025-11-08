using System.Collections;
using UnityEngine;

public class WaveCreator : MonoBehaviour
{
    private EnemyPool enemyPool;
    private LevelData levelData;
    private PlayerController playerController;

    private int currentWave = 0;
    private float elapsedTime = 0f;
    private bool isRunning = false;

    private void Awake()
    {
        enemyPool = EnemyPool.Instance;
        levelData = LevelManager.I.CurrentLevelData;
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void Start()
    {
        StartCoroutine(WaveRoutine());
    }

    private IEnumerator WaveRoutine()
    {
        isRunning = true;
        yield return new WaitForSeconds(1f);

        while (elapsedTime < levelData.totalSeconds)
        {
            currentWave++;
            SpawnWave(currentWave);
            yield return new WaitForSeconds(levelData.timeBetweenWaves);
            elapsedTime += levelData.timeBetweenWaves;
        }

        isRunning = false;
    }

    private void SpawnWave(int waveIndex)
    {
        int enemyCount = levelData.baseEnemyCount + (waveIndex - 1) * levelData.enemyCountIncrement;
        StartCoroutine(SpawnEnemiesWithDelay(enemyCount));
    }

    private IEnumerator SpawnEnemiesWithDelay(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPos = GetSpawnPosition();
            var enemy = enemyPool.GetFromPool();
            enemy.transform.position = spawnPos;
            enemy.transform.rotation = Quaternion.LookRotation(playerController.transform.position - spawnPos);
            enemy.Init(playerController);
            enemy.gameObject.SetActive(true);

            yield return new WaitForSeconds(levelData.spawnInterval);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        // Oyuncunun etrafında rastgele bir konum üret
        Vector2 randomCircle = Random.insideUnitCircle.normalized * Random.Range(levelData.spawnRadius * 0.8f, levelData.spawnRadius);
        Vector3 spawnPos = playerController.transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);

        // Eğer spawn yeri zeminin altına düşerse biraz yukarı al (güvenlik)
        spawnPos.y = 0.1f;
        return spawnPos;
    }
}
