using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    [SerializeField] private PlayerControl player;

    private GameStates gameState = GameStates.MainMenu;
    

    private int coinCount;
    private int enemiesKilled;
    private int currentWaveEnemiesKilled = 0;
    private int finalMultipliedScore;
    public int scoreMultiplier;

    public float difficultyMultiplier;


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
        GameStates lastState = gameState;
        gameState = newState;
        Debug.Log($"Current game state: {gameState}, last state {lastState}");
        
        // Activate correct UIs
        switch (gameState)
        {
            default:
            case GameStates.Gameplay:
                ScoreUI.Instance.ToggleScoreUI(true);
                StartUI.Instance.ToggleActive(false);
                EndScreenUI.Instance.ToggleActive(false);
                break;

            case GameStates.MainMenu:
                ScoreUI.Instance.ToggleScoreUI(false);
                StartUI.Instance.ToggleActive(true);
                EndScreenUI.Instance.ToggleActive(false);
                break;
            
            case GameStates.EndScreen:
                ScoreUI.Instance.ToggleScoreUI(false);
                StartUI.Instance.ToggleActive(false);
                EndScreenUI.Instance.ToggleActive(true);
                EndScreenUI.Instance.SetScoreText(finalMultipliedScore);
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
        currentWaveEnemiesKilled += increaseAmount;
        finalMultipliedScore = enemiesKilled * scoreMultiplier;
        ScoreUI.Instance.UpdateScore(finalMultipliedScore);

        int currentMaxEnemies = EntityManager.Instance.GetMaxEnemies();

        if (currentWaveEnemiesKilled >= currentMaxEnemies) // Need to increase difficulty.
        {
            EntityManager.Instance.SetMaxEnemies(Mathf.CeilToInt(currentMaxEnemies * difficultyMultiplier));
            EntityManager.Instance.SetMaxSummons(EntityManager.Instance.GetMaxSummons() + 1);
            currentWaveEnemiesKilled = 0;
        }
    }


    public void IncreaseCoins(int increaseAmount)
    {
        coinCount += increaseAmount;
        ScoreUI.Instance.UpdateCoinCount(coinCount);
    }

    private void ClearScoreData()
    {
        player.ResetHealth();
        enemiesKilled = 0;
        ScoreUI.Instance.UpdateScore(enemiesKilled);
        coinCount = 0;
        ScoreUI.Instance.UpdateCoinCount(coinCount);
    }


    public void ResetGameData()
    {
        EntityManager.Instance.ClearAllEntities();
        ClearScoreData();
        SetGameState(GameStates.Gameplay);
    }



}

public enum GameStates
{
    MainMenu,
    Gameplay,
    EndScreen
}
