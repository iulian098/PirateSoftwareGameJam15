using TMPro;
using UnityEngine;

public class UI_PotionSlot : UI_Slot
{
    [SerializeField] TMP_Text amountText;

    public override void SetItem(ItemData item) {
        base.SetItem(item);

        UpdateUI();
    }

    public override void SetItem(ItemData item, int amount) {
        base.SetItem(item, amount);
        UpdateUI();
    }

    public override void UpdateUI() {
        if (item == null) {
            iconImage.gameObject.SetActive(false);
            amountText.gameObject.SetActive(false);
            return;
        }
        else {
            iconImage.gameObject.SetActive(true);
            amountText.gameObject.SetActive(true);
        }

        iconImage.sprite = item.Icon;
        amountText.text = $"x{amount}";
    }
}
