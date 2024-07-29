using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button newGameButton;
    [SerializeField] Button continueButton;
    [SerializeField] int newGameLevel;

    private void Start() {
        SaveSystem.Instance.OnNewGameBegin += LoadNextScene;
    }

    private void OnDestroy() {
        SaveSystem.Instance.OnNewGameBegin -= LoadNextScene;
    }

    public void Continue() {
        SaveSystem.Instance.ContinueGame();
        LoadNextScene();
    }

    public void NewGame() {
        SaveSystem.Instance.NewGame();
    }

    void LoadNextScene() {
        SceneManager.LoadScene(newGameLevel);
    }
}
