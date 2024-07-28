using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] UI_HealthBar playerHealthBar;
    [SerializeField] EnemyHealthBarManager enemyHealthBarManager;
    [SerializeField] UI_DamageNumberManager damageNumberManager;
    [SerializeField] ItemInfo itemInfo;
    [SerializeField] ItemDragIcon itemDragIcon;
    [SerializeField] ItemPickupInfo itemPickupInfo;
    [SerializeField] TMP_Text infoText;

    public UI_HealthBar PlayerHealthBar => playerHealthBar;
    public EnemyHealthBarManager EnemyHealthBarManager => enemyHealthBarManager;
    public UI_DamageNumberManager DamageNumberManager => damageNumberManager;
    public ItemInfo ItemInfo => itemInfo;
    public ItemDragIcon ItemDragIcon => itemDragIcon;
    public ItemPickupInfo ItemPickupInfo => itemPickupInfo;

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
}
