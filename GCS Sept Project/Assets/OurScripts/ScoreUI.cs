using System;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI Instance {get; private set;}

    [SerializeField] private GameObject rootGameObjectToHide;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text coinCountText;
    [SerializeField] private TMP_Text summonsCountText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ToggleScoreUI(false);
    }


    public void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    public void UpdateCoinCount(int newCoinCount)
    {
        coinCountText.text = newCoinCount.ToString();
    }

    public void ToggleScoreUI(bool setActive)
    {
        rootGameObjectToHide.SetActive(setActive);
    }

    public void SetSummonsCount(int currentSummons, int maxSummons)
    {
        summonsCountText.text = currentSummons + " / " + maxSummons;
    }

}
