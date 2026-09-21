using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>, IKeyIcon
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


    Queue<UIPanel> activePanelsQueue = new Queue<UIPanel>();

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

    public void ShowPanel(UIPanel panel)
    {
        if (activePanelsQueue.Contains(panel))
        {
            Debug.LogError($"{panel.name} already exists");
            return;
        }
        else
        {
            panel.Show();
            activePanelsQueue.Enqueue(panel);
        }
    }

    public void HideLastPanel()
    {
        if (activePanelsQueue.Count == 0)
            return;

        var lastPanel = activePanelsQueue.Dequeue();
        lastPanel.Hide();
    }

    public void DeviceChanged(InputDeviceType deviceType)
    {
        
    }
}
