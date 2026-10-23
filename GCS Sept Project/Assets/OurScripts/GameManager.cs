using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    private GameStates gameState = GameStates.MainMenu;

    private int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameStates GetGameState()
    {
        return gameState;
    }

    public void SetGameState(GameStates newState)
    {
        gameState = newState;
        Debug.Log($"Current game state: {gameState}");
    }

    public bool IsInGamePlay()
    {
        return gameState == GameStates.Gameplay;
    }

    public void IncreaseScore(int increaseAmount)
    {
        score += increaseAmount;
    }
}

public enum GameStates
{
    MainMenu,
    Gameplay,
    EndScreen
}
