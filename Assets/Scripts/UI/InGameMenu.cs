using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : UIPanel
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

    public override void Show() {
        content.SetActive(true);
        Time.timeScale = 0;
    }

    public override void Hide() {
        content.SetActive(false);
        Time.timeScale = 1;
    }
}
