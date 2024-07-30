using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] GameObject content;

    public bool IsActive => content.activeSelf;

    public void Resume() {
        Hide();
    }

    public void GoToMainMenu() {
        Hide();
        SceneManager.LoadScene(0);
    }

    public void Show() {
        content.SetActive(true);
        Time.timeScale = 0;
    }

    public void Hide() {
        content.SetActive(false);
        Time.timeScale = 1;
    }
}
