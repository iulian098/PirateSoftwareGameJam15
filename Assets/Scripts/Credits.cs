using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] TMP_Text creditsText;
    [SerializeField] ScrollRect scroll;
    [SerializeField] float startingTime;
    [SerializeField] float scrollTime;
    [SerializeField] float resumeTime;

    float currentTime;
    bool initialized;
    bool isDragging;
    // Start is called before the first frame update
    void Start()
    {
        creditsText.text = Resources.Load<TextAsset>("Credits").text;
        scroll.verticalNormalizedPosition = 0;
        scroll.content.position = new Vector3(scroll.content.position.x, 0);
        initialized = true;
        currentTime = scrollTime;
        MouseHelper.Instance.OnDrag += OnDrag;
        MouseHelper.Instance.OnDrop += OnDrop;
    }

    private void OnDrop() {
        isDragging = false;
        currentTime = scroll.verticalNormalizedPosition * scrollTime;
    }

    private void OnDrag() {
        isDragging = true;
        startingTime = resumeTime;
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene(0);
    }

    private void Update() {
        if (!initialized || isDragging) return;

        if (startingTime > 0) {
            startingTime -= Time.deltaTime;
            return;
        }

        scroll.verticalNormalizedPosition = currentTime / scrollTime;

        if (currentTime > 0)
            currentTime -= Time.deltaTime;

        if (currentTime < 0)
            currentTime = 0;


    }
}
