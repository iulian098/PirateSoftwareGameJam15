using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] UI_HealthBar playerHealthBar;
    [SerializeField] EnemyHealthBarManager enemyHealthBarManager;
    [SerializeField] UI_DamageNumberManager damageNumberManager;
    [SerializeField] ItemInfo itemInfo;
    [SerializeField] ItemDragIcon itemDragIcon;
    [SerializeField] ItemPickupInfo itemPickupInfo;
    [SerializeField] TMP_Text infoText;
    [SerializeField] GameObject deathScreen;
    [SerializeField] InGameMenu inGameMenu;

    public UI_HealthBar PlayerHealthBar => playerHealthBar;
    public EnemyHealthBarManager EnemyHealthBarManager => enemyHealthBarManager;
    public UI_DamageNumberManager DamageNumberManager => damageNumberManager;
    public ItemInfo ItemInfo => itemInfo;
    public ItemDragIcon ItemDragIcon => itemDragIcon;
    public ItemPickupInfo ItemPickupInfo => itemPickupInfo;

    InputAction backAction;

    private void Start() {
        backAction = InGameManager.Instance.PlayerInput.actions["Back"];
    }

    private void Update() {
        if (backAction.WasPerformedThisFrame()) {
            if (inGameMenu.IsActive)
                inGameMenu.Hide();
            else
                inGameMenu.Show();
        }
    }

    public void ShowPickupInfo(ItemData item, int amount) {
        ItemPickupInfo.AddItemInfo(item, amount);
    }

    public void ShowInfoText(string info) {
        infoText.text = info;
        infoText.gameObject.SetActive(true);
    }

    public void HideInfo() {
        infoText.gameObject.SetActive(false);
    }

    public void ShowDheathScreen() {
        GlobalData.isPaused = true;
        deathScreen.SetActive(true);
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene(0);
    }
}
