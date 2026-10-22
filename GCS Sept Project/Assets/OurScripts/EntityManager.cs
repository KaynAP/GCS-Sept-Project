using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance { get; private set; }
    private List<GameObject> SummonsList = new List<GameObject>();

    [SerializeField] private int maxSummons = 1;

    [SerializeField] private PlayerControl player;


    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
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


    private void SpawnEnemy()
    {
        
    }



}
