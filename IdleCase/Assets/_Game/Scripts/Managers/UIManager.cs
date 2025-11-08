using System;
// using Lofelt.NiceVibrations;
// using PrimeTween;
using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private GlobalEventsSO globalEventsSO;
    [SerializeField] private GameObject _levelEndBackground;
    [SerializeField] private GameObject _settingsPanel, _winPanel, _losePanel;
    [SerializeField] private TextMeshProUGUI levelText;
    private GameManager _gameManager;
    private bool retryCooldown = false;

    private void OnEnable()
    {
        _gameManager = GameManager.Instance;
        _gameManager.OnGameStateChanged += GameStateChanged;
    }

    private void OnDisable()
    {
        _gameManager.OnGameStateChanged -= GameStateChanged;
    }

    public void RetryButtonClicked()
    {
        // Butona sürekli tıklanmasını engelle
        if (retryCooldown) return;
        retryCooldown = true;
        //Tween.Delay(0.5f, () => retryCooldown = false);

        //globalEventsSO.HapticEvents.OnHaptic?.Invoke(HapticPatterns.PresetType.Selection);
        LevelManager.I.LoadLevel();
    }

    private void GameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Initialized:
                levelText.text = "Level " + (SaveLoad.I.playerProgress.currentLevel + 1).ToString();
                _winPanel.SetActive(false);
                _losePanel.SetActive(false);
                _levelEndBackground.SetActive(false);
                break;
            case GameState.Started:
                levelText.text = "Level " + (SaveLoad.I.playerProgress.currentLevel + 1).ToString();
                break;
            case GameState.Win:
                // Tween.Delay(1f, () =>
                // {
                //     _winPanel.SetActive(true);
                //     _levelEndBackground.SetActive(true);
                // });
                break;
            case GameState.Lose:
                // Tween.Delay(.1f, () =>
                // {
                //     _losePanel.SetActive(true);
                //     _levelEndBackground.SetActive(true);
                // });
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }
    }

    public void SetSettingsPanel(bool isActive)
    {
        //globalEventsSO.HapticEvents.OnHaptic?.Invoke(HapticPatterns.PresetType.Selection);
        _settingsPanel.SetActive(isActive);

        // set game time scale to zero when settings panel is active
        if (isActive)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}