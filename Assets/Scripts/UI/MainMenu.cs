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
        int lastScene = UserManager.playerData.GetInt("LastLevel", 1);
        SceneManager.LoadScene(lastScene);
    }

    public void NewGame() {
        SaveSystem.Instance.NewGame();
    }

    void LoadNextScene() {
        CommonPopup.Instance.Show("You're an Alchemist from the far South on a journey to gather a special medicinal plant.\r\n" +
            "You need that plant in order to produce a cure for your daughter which had fallen sick after the Lych in the North was slain.\r\n" +
            "Albeit the potency of this plant was just a rumor, you're willing to take your chances for the sake of your family.\r\n" +
            "These rumors led you to the Dungeon in the far North and now you find yourself on a path through a graveyard before said dungeon.",
           () => { SceneManager.LoadScene(newGameLevel); }, "Ok");
    }


}
