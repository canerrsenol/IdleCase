using System;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] private GlobalEventsSO globalEventsSO;
    private CountdownTimer countDownTimer;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void OnEnable()
    {
        gameManager.OnGameStateChanged += OnGamePhaseChanged;
    }

    private void OnDisable()
    {
        if (gameManager != null) gameManager.OnGameStateChanged -= OnGamePhaseChanged;
    }

    private void OnGamePhaseChanged(GameState gamePhase)
    {
        if (gamePhase == GameState.Initialized)
        {
            if (countDownTimer != null)
            {
                countDownTimer.OnTimerStop -= OnTimerStop;
            }

            var totalSeconds = LevelManager.I.CurrentLevelData.totalSeconds;
            globalEventsSO.UIEvents.RemainingTime?.Invoke((int)totalSeconds);
            countDownTimer = new CountdownTimer(totalSeconds);
            countDownTimer.OnTimerStop += OnTimerStop;
        }
        if (gamePhase == GameState.Started)
        {
            countDownTimer?.Start();
        }
    }
    
    private void Update()
    {
        if (countDownTimer != null && countDownTimer.IsRunning && gameManager.GameState == GameState.Started) 
        {
            countDownTimer.Tick(Time.deltaTime);
            globalEventsSO.UIEvents.RemainingTime.Invoke((int)countDownTimer.Time);
        }
    }

    private void OnTimerStop()
    {
        gameManager.ChangeGameState(GameState.Win);
    }
}