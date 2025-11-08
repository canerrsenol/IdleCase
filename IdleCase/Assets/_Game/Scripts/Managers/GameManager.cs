using System;

public class GameManager : MonoSingleton<GameManager>
{
    public GameState GameState;
    public event Action<GameState> OnGameStateChanged;
    public GlobalEventsSO globalEvents;

    public void CompleteLevel()
    {
        ChangeGameState(GameState.Win);
    }

    public void ChangeGameState(GameState newGameState)
    {
        if (GameState == newGameState)
            return;
        
        switch (newGameState)
        {
            case GameState.Initialized:
                break;
            case GameState.Started:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
        }
        if (GameState != newGameState)
            OnGameStateChanged?.Invoke(newGameState);
        GameState = newGameState;
    }
}

public enum GameState
{
    Splash,
    Initialized,
    Started,
    Win,
    Lose
}