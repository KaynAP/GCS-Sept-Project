using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenUI : MonoBehaviour
{

    public static EndScreenUI Instance {get; private set;}

    [SerializeField] private GameObject rootObjectToHide;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TMP_Text score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        playAgainButton.onClick.AddListener(PlayAgainButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);

        ToggleActive(false);
    }


    public void ToggleActive(bool active)
    {
        rootObjectToHide.SetActive(active);
    }

    private void PlayAgainButtonClicked()
    {
        GameManager.Instance.ResetGameData();
    }

    public void SetScoreText(int finalScore)
    {
        score.text = finalScore.ToString();
    }

    private void QuitButtonClicked()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
