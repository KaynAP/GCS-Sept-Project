using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance { get; private set; }
    private List<GameObject> SummonsList = new List<GameObject>();

    [SerializeField] private int maxSummons = 1;
    [SerializeField] private Transform enemyPrefab;

    [SerializeField] private PlayerControl player;
    [SerializeField] private LayerMask spawnableLayer;

    private int directionsCount = 4;
    private int offCameraDistanceVertical = 10;
    private int offCameraDistanceHorizontal = 17;
    private int otherAxisOffset = 2;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
    }


    private void Start()
    {
        GameInput.Instance.OnTestKeyPressed += InputActions_TestKeyPressed;
    }

    private void InputActions_TestKeyPressed(object sender, EventArgs e)
    {
        SpawnEnemyAtValidLocation();
    }

    public PlayerControl GetPlayer() {
        return player;
    }


    #region Summon Stuff
    public int GetNumberOfSummons()
    {
        return SummonsList.Count;
    }

    public void AddToSpawnList(GameObject summon)
    {
        SummonsList.Add(summon);
    }

    public void SetMaxSummons(int newMax)
    {
        maxSummons = newMax;
    }

    public bool CanSummonMore()
    {
        return SummonsList.Count < maxSummons;
    }

    #endregion


    private void SpawnEnemyAtValidLocation()
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
            return;
        } else
        {
            // Spawn enemy! We found a spot :)
            Instantiate(enemyPrefab, validSpawnLocation, Quaternion.identity);
        }
    }

    private Vector3 FindRandomSpawnPosition()
    {
        int directionToCheck = UnityEngine.Random.Range(1, directionsCount + 1);

        Vector3 positionToCheck;
        int randomOtherAxisOffset = UnityEngine.Random.Range(1, otherAxisOffset);
        switch (directionToCheck)
        {
            default:
            case 1: // Up!
                positionToCheck = GetPlayer().transform.position + new Vector3(randomOtherAxisOffset, offCameraDistanceVertical, 0);
                Debug.Log($"Checking {positionToCheck}");
                if (IsValidPosition(positionToCheck))
                {
                    return positionToCheck;
                }
                break;

            case 2: // Down!
                positionToCheck = GetPlayer().transform.position + new Vector3(randomOtherAxisOffset, -offCameraDistanceVertical, 0);
                Debug.Log($"Checking {positionToCheck}");
                if (IsValidPosition(positionToCheck))
                {
                    return positionToCheck;
                }
                break;

            case 3: // Left!
                positionToCheck = GetPlayer().transform.position + new Vector3(-offCameraDistanceHorizontal, randomOtherAxisOffset, 0);
                Debug.Log($"Checking {positionToCheck}");
                if (IsValidPosition(positionToCheck))
                {
                    return positionToCheck;
                }
                break;

            case 4: // Right! 
                positionToCheck = GetPlayer().transform.position + new Vector3(offCameraDistanceHorizontal, randomOtherAxisOffset, 0);
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

}

