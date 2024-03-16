using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string startButtonSceneName;
    public string setupButtonSceneName;

    public Button startButton;
    public Button setupButton;
    public GameObject mainMenu;
    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);
        setupButton.onClick.AddListener(SetupGame);
    }

    private void OnDestroy()
    {
        startButton.onClick.RemoveListener(StartGame);
        setupButton.onClick.RemoveListener(SetupGame);
    }
    private void StartGame()
    {
        mainMenu.SetActive(false);
        LevelManager.Instance.LoadSceneAsync(startButtonSceneName);
    }
    private void SetupGame()
    {
        mainMenu.SetActive(false);
        LevelManager.Instance.LoadSceneAsync(setupButtonSceneName);
    }
}