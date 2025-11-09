using UnityEngine;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private GlobalEventsSO globalEvents;

    private SaveManager saveManager;

    void Awake()
    {
        saveManager = SaveManager.Instance;
    }

    void OnEnable()
    {
        globalEvents.EnemyEvents.OnEnemyDeath += OnEnemyDeath;
    }

    void OnDisable()
    {
        globalEvents.EnemyEvents.OnEnemyDeath -= OnEnemyDeath;
    }

    private void OnEnemyDeath()
    {
        saveManager.playerProgress.totalDefeatedEnemyCount++;
    }

    void OnApplicationPause(bool pause)
    {
        saveManager.SaveToJson();
    }

    void OnApplicationQuit()
    {
        saveManager.SaveToJson();
    }
}
