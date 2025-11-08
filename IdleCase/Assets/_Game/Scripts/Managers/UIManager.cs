using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private GlobalEventsSO globalEventsSO;
    [SerializeField] private GameObject _levelEndBackground;
    [SerializeField] private GameObject  _winPanel, _losePanel;
    [SerializeField] private TextMeshProUGUI levelText;
    private GameManager gameManager;
    private SaveManager saveManager;

    private void OnEnable()
    {
        gameManager = GameManager.Instance;
        saveManager = SaveManager.Instance;
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
                levelText.text = "Level " + (saveManager.playerProgress.currentLevel + 1).ToString();
                _winPanel.SetActive(false);
                _losePanel.SetActive(false);
                _levelEndBackground.SetActive(false);
                break;
            case GameState.Started:
                levelText.text = "Level " + (saveManager.playerProgress.currentLevel + 1).ToString();
                break;
            case GameState.Win:
                _winPanel.SetActive(true);
                _levelEndBackground.SetActive(true);
                break;
            case GameState.Lose:
                _losePanel.SetActive(true);
                _levelEndBackground.SetActive(true);
                break;
        }
    }
}