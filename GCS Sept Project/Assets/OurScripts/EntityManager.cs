using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance { get; private set; }
    private List<GameObject> summonsList = new List<GameObject>();
    private List<Enemy> currentEnemiesList = new List<Enemy>();
    private List<GameObject> coinEntitiesList = new List<GameObject>();
    public int baseMaxSummons;
    public int baseMaxEnemies;
    public float baseChanceToDropCoin;
    public float baseChanceToDoubleCoin;

    private int maxSummons;
    private int maxEnemies;
    private float currentChanceToDropCoin;
    private float currentChanceToDoubleCoin;
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private Transform coinPrefab;

    [SerializeField] private PlayerControl player;
    [SerializeField] private LayerMask spawnableLayer;

    private int directionsCount = 4;
    private int offCameraDistanceVertical = 10;
    private int offCameraDistanceHorizontal = 17;
    private int otherAxisOffset = 8;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
    }


    private void Start()
    {
        GameInput.Instance.OnTestKeyPressed += InputActions_TestKeyPressed;

        maxSummons = baseMaxSummons;
        maxEnemies = baseMaxEnemies;
        currentChanceToDropCoin = baseChanceToDropCoin;
        currentChanceToDoubleCoin = baseChanceToDoubleCoin;
    }


    private void Update()
    {
        if (GameManager.Instance.GetGameState() == GameStates.Gameplay && currentEnemiesList.Count < maxEnemies)
        {
            Enemy spawnedEnemy = SpawnEnemyAtValidLocation();
            if (spawnedEnemy != null)
            {
                currentEnemiesList.Add(spawnedEnemy);
            }
        }
    }


    private void InputActions_TestKeyPressed(object sender, EventArgs e)
    {
        Enemy spawnedEnemy = SpawnEnemyAtValidLocation();
        if (spawnedEnemy != null)
        {
            currentEnemiesList.Add(spawnedEnemy);
        }
    }

    public PlayerControl GetPlayer() {
        return player;
    }

    public List<Enemy> GetEnemiesList()
    {
        return currentEnemiesList;
    }


    #region Summon Stuff
    public int GetNumberOfSummons()
    {
        return summonsList.Count;
    }

    public void AddToSummonsList(GameObject summon)
    {
        summonsList.Add(summon);
    }

    public void RemoveFromSummonsList(GameObject summon)
    {
        summonsList.Remove(summon);
    }

    public void SetMaxSummons(int newMax)
    {
        maxSummons = newMax;
    }

    public int GetMaxEnemies()
    {
        return maxEnemies;
    }

    public void SetMaxEnemies(int newMax)
    {
        maxEnemies = newMax;
    }

    public bool CanSummonMore()
    {
        return summonsList.Count < maxSummons;
    }

    #endregion


    #region Enemy Spawn Logic

    private Enemy SpawnEnemyAtValidLocation()
    {
        Vector3 validSpawnLocation = Vector3.zero;

        int maxLoops = 100;
        int i = 0;
        while (validSpawnLocation == Vector3.zero && i < maxLoops)
        {
            i++;
            validSpawnLocation = FindRandomSpawnPosition();
        }

        if (validSpawnLocation == Vector3.zero) {
            Debug.LogError("Dang you still didn't find a valid random spawn???");
            return null;
        } else
        {
            // Spawn enemy! We found a spot :)
            Enemy newEnemy = Instantiate(enemyPrefab, validSpawnLocation, Quaternion.identity).GetComponent<Enemy>();
            return newEnemy;
        }
    }

    private Vector3 FindRandomSpawnPosition()
    {
        int directionToCheck = UnityEngine.Random.Range(1, directionsCount + 1);

        Vector3 positionToCheck;
        int randomOtherAxisOffset = UnityEngine.Random.Range(1, otherAxisOffset);
        int otherAxisDirection = UnityEngine.Random.Range(0,2)*2-1;
        switch (directionToCheck)
        {
            default:
            case 1: // Up!
                positionToCheck = GetPlayer().transform.position + new Vector3(otherAxisDirection * randomOtherAxisOffset, offCameraDistanceVertical, 0);
                Debug.Log($"Checking {positionToCheck}");
                if (IsValidPosition(positionToCheck))
                {
                    return positionToCheck;
                }
                break;

            case 2: // Down!
                positionToCheck = GetPlayer().transform.position + new Vector3(otherAxisDirection * randomOtherAxisOffset, -offCameraDistanceVertical, 0);
                Debug.Log($"Checking {positionToCheck}");
                if (IsValidPosition(positionToCheck))
                {
                    return positionToCheck;
                }
                break;

            case 3: // Left!
                positionToCheck = GetPlayer().transform.position + new Vector3(-offCameraDistanceHorizontal, otherAxisDirection * randomOtherAxisOffset, 0);
                Debug.Log($"Checking {positionToCheck}");
                if (IsValidPosition(positionToCheck))
                {
                    return positionToCheck;
                }
                break;

            case 4: // Right! 
                positionToCheck = GetPlayer().transform.position + new Vector3(offCameraDistanceHorizontal, otherAxisDirection * randomOtherAxisOffset, 0);
                Debug.Log($"Checking {positionToCheck}");
                if (IsValidPosition(positionToCheck))
                {
                    return positionToCheck;
                }
                break;
        }

        return Vector3.zero;
    }

    private bool IsValidPosition(Vector2 raycastPosition)
    {
        Collider2D hit = Physics2D.OverlapPoint(raycastPosition, spawnableLayer);
        if (hit != null) {
            return true;
        } else
        {
            return false;
        }
    }

    #endregion



    public void ClearAllEntities()
    {
        ClearAndDeleteSummons();
        ClearAndDeleteEnemies();
        ClearAndDeleteCoins();
        SetMaxSummons(baseMaxSummons);
        maxEnemies = baseMaxEnemies;
        currentChanceToDropCoin = baseChanceToDropCoin;
        currentChanceToDoubleCoin = baseChanceToDoubleCoin;
        player.transform.position = Vector3.zero;
    }


    private void ClearAndDeleteSummons()
    {
        foreach (GameObject summon in summonsList)
        {
            Destroy(summon);
        }
        summonsList = new List<GameObject>();
    }


    private void ClearAndDeleteEnemies()
    {
        foreach (Enemy enemy in currentEnemiesList)
        {
            Destroy(enemy.gameObject);
        }
        currentEnemiesList = new List<Enemy>();
    }

    private void ClearAndDeleteCoins()
    {
        foreach (GameObject coin in coinEntitiesList)
        {
            Destroy(coin.gameObject);
        }
        coinEntitiesList = new List<GameObject>();
    }

    public void RemoveCoinFromList(GameObject coinToRemove)
    {
        coinEntitiesList.Remove(coinToRemove);
    }

    public void KillEnemy(Enemy enemy)
    {
        GameManager.Instance.IncreaseScore(1);

        bool didCoinSpawn = UnityEngine.Random.Range(0, 11) * 0.1f <= currentChanceToDropCoin;
        bool didCoinDouble = UnityEngine.Random.Range(0, 11) * 0.1f <= currentChanceToDoubleCoin;

        if (didCoinSpawn && didCoinDouble)
        {
            SpawnCoinOnEnemy(enemy);
            SpawnCoinOnEnemy(enemy);
        } else if (didCoinSpawn)
        {
            SpawnCoinOnEnemy(enemy);
        }


        currentEnemiesList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private void SpawnCoinOnEnemy(Enemy enemy)
    {
        int randomOffsetX = UnityEngine.Random.Range(-1, 1);
        int randomOffsetY = UnityEngine.Random.Range(-1, 1);
        Vector3 spawnPosition = new Vector3(enemy.transform.position.x + randomOffsetX, enemy.transform.position.y + randomOffsetY, 0);
        Transform coin = Instantiate(coinPrefab, spawnPosition, enemy.transform.rotation);
        coinEntitiesList.Add(coin.gameObject);
    }

}

