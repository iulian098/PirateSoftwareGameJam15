using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int newGameLevel;
    public void Continue() {

    }

    public void NewGame() {
        SceneManager.LoadScene(newGameLevel);
    }
}
