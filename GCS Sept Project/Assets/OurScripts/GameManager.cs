using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    private GameStates gameState = GameStates.MainMenu;

    private int coinCount;
    private int enemiesKilled;
    private int finalMultipliedScore;
    public int scoreMultiplier;


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
        
        
        // Activate correct UIs
        switch (gameState)
        {
            default:
            case GameStates.Gameplay:
                ScoreUI.Instance.ToggleScoreUI(true);
                break;

            case GameStates.MainMenu:
                ScoreUI.Instance.ToggleScoreUI(false);
                break;
            
            case GameStates.EndScreen:
                ScoreUI.Instance.ToggleScoreUI(false);
                break;
        }

    }

    public bool IsInGamePlay()
    {
        return gameState == GameStates.Gameplay;
    }

    public void IncreaseScore(int increaseAmount)
    {
        enemiesKilled += increaseAmount;
        finalMultipliedScore = enemiesKilled * scoreMultiplier;
        ScoreUI.Instance.UpdateScore(finalMultipliedScore);
    }

    public void IncreaseCoins(int increaseAmount)
    {
        coinCount += increaseAmount;
        ScoreUI.Instance.UpdateCoinCount(coinCount);
    }
}

public enum GameStates
{
    MainMenu,
    Gameplay,
    EndScreen
}
