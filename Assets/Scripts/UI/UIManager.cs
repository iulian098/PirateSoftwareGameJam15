using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>, IDeviceChanged
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


    List<UIPanel> activePanelsQueue = new List<UIPanel>();
    Dictionary<Type, UIPanel> activePanelsDictionary = new Dictionary<Type, UIPanel>();

    public UI_HealthBar PlayerHealthBar => playerHealthBar;
    public EnemyHealthBarManager EnemyHealthBarManager => enemyHealthBarManager;
    public UI_DamageNumberManager DamageNumberManager => damageNumberManager;
    public ItemInfo ItemInfo => itemInfo;
    public ItemDragIcon ItemDragIcon => itemDragIcon;
    public ItemPickupInfo ItemPickupInfo => itemPickupInfo;

    InputAction backAction;
    string currentText;

    private void Start() {
        backAction = InGameManager.Instance.PlayerInput.actions["Back"];
    }

    private void Update() {
        if (backAction.WasPerformedThisFrame()) {
            if (activePanelsQueue.Count == 0)
            {
                if (inGameMenu.IsActive)
                {
                    HideLastPanel();
                }
                else
                {
                    ShowPanel(inGameMenu);
                    Cursor.visible = true;
                }
            }
            else
            {
                HideLastPanel();
            }
        }
    }

    public void ShowPickupInfo(ItemData item, int amount) {
        ItemPickupInfo.AddItemInfo(item, amount);
    }

    public void ShowInfoText(string info) {
        currentText = info;
        var keyBindData = UIKeyIconManager.Instance.GetActionIconText(InGameManager.Instance.PlayerInput.actions["Use"]);

        infoText.text = currentText.Replace("{Key}", keyBindData);

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
        GlobalData.isPaused = false;
        SceneManager.LoadScene(0);
    }

    public void ShowPanel<T>(T panel, bool activatePanel = true) where T : UIPanel
    {
        if (activePanelsQueue.Contains(panel))
        {
            Debug.LogError($"{panel.name} already exists");
            return;
        }
        else
        {
            panel.Show();
            if (activatePanel)
                panel.gameObject.SetActive(true);
            activePanelsQueue.Add(panel);
            activePanelsDictionary.Add(panel.GetType(), panel);
        }
    }

    public void HidePanel<T>(Type panel, bool deactivatePanel = true)
    {
        if(activePanelsDictionary.TryGetValue(typeof(T), out UIPanel existingPanel))
        {
            activePanelsQueue.Remove(existingPanel);
            activePanelsDictionary.Remove(typeof(T));
            existingPanel.Hide();
            if (deactivatePanel)
                existingPanel.gameObject.SetActive(false);
        }
    }

    public void HideLastPanel(bool deactivatePanel = true)
    {
        if (activePanelsQueue.Count == 0)
            return;

        var lastPanel = activePanelsQueue[0];
        lastPanel.Hide();

        if (deactivatePanel)
            lastPanel.gameObject.SetActive(false);

        activePanelsDictionary.Remove(lastPanel.GetType());
        activePanelsQueue.RemoveAt(0);
    }

    public UIPanel GetPanel<T>(Type panel)
    {
        if (activePanelsDictionary.TryGetValue(typeof(T), out var foundPanel))
            return foundPanel;
        return null;
    }

    public void DeviceChanged(InputDeviceType deviceType)
    {
        
    }
}
