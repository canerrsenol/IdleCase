using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    public PlayerProgress playerProgress;
    private const string PlayerSaveKey = "PlayerSave";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(PlayerSaveKey))
        {
            LoadFromJson();
        }
        else
        {
            playerProgress = new PlayerProgress
            {
                currentLevel = 0,
                secondLevelListIndex = 0,
                totalDefeatedEnemyCount = 0
            };
            SaveToJson();
        }
    }

    public void SaveToJson()
    {
        string content = JsonUtility.ToJson(playerProgress);
        PlayerPrefs.SetString(PlayerSaveKey, content);
        PlayerPrefs.Save();
    }

    public void LoadFromJson()
    {
        if (PlayerPrefs.HasKey(PlayerSaveKey))
        {
            string content = PlayerPrefs.GetString(PlayerSaveKey);
            playerProgress = JsonUtility.FromJson<PlayerProgress>(content);
        }
    }
}
