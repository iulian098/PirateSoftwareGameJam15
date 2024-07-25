using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickupEntry : MonoBehaviour
{
    readonly int InHash = Animator.StringToHash("In");
    readonly int OutHash = Animator.StringToHash("Out");
    [SerializeField] Image iconImage;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text amountText;
    [SerializeField] Animator animator;

    public Action<ItemPickupEntry> OnFadedOut;

    public void Init(ItemData item, int amount) {
        iconImage.sprite = item.Icon;
        itemName.text = item.ItemName;
        amountText.text = $"+{amount}";
        animator.Play(InHash);
        Invoke(nameof(StartFade), 1.5f);
    }

    public void StartFade() {
        animator.Play(OutHash);
    }

    public void OnFinished() {
        OnFadedOut?.Invoke(this);
    }
}
