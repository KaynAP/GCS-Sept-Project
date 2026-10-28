using System;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public static StartUI Instance {get; private set;}

    [SerializeField] private GameObject rootObjectToHide;
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;


    private void Awake(){
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
    }


    public void ToggleActive(bool active)
    {
        rootObjectToHide.SetActive(active);
    }

    private void PlayGame()
    {
        rootObjectToHide.SetActive(false);
        GameManager.Instance.SetGameState(GameStates.Gameplay);
    }

    private void QuitGame()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
