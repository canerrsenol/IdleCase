using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private GlobalEventsSO globalEventsSO;
    [SerializeField] private GameObject levelEndBackground;
    [SerializeField] private GameObject  winPanel, losePanel, inGamePanel;
    [SerializeField] private TextMeshProUGUI levelText;
    private GameManager gameManager;
    private SaveManager saveManager;
    private LevelManager levelManager;

    private void OnEnable()
    {
        gameManager = GameManager.Instance;
        saveManager = SaveManager.Instance;
        levelManager = LevelManager.Instance;
        gameManager.OnGameStateChanged += GameStateChanged;
    }

    private void OnDisable()
    {
        gameManager.OnGameStateChanged -= GameStateChanged;
    }

    private void GameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Initialized:
                winPanel.SetActive(false);
                losePanel.SetActive(false);
                levelEndBackground.SetActive(false);
                break;
            case GameState.Started:
                winPanel.SetActive(false);
                losePanel.SetActive(false);
                levelEndBackground.SetActive(false);
                inGamePanel.SetActive(true);
                levelText.text = "Level " + (saveManager.playerProgress.currentLevel + 1).ToString();
                break;
            case GameState.Win:
                winPanel.SetActive(true);
                levelEndBackground.SetActive(true);
                inGamePanel.SetActive(false);
                break;
            case GameState.Lose:
                losePanel.SetActive(true);
                levelEndBackground.SetActive(true);
                inGamePanel.SetActive(false);
                break;
        }
    }

    public void RestartLevel()
    {
        levelManager.LoadLevel();
    }

    public void NextLevel()
    {
        levelManager.LoadNextLevel();
    }
}