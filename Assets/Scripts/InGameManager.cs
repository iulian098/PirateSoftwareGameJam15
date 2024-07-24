using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameManager : MonoSingleton<InGameManager>
{
    [SerializeField] InGameData inGameData;
    [SerializeField] Player player;
    [SerializeField] PlayerInput playerInput;

    public InGameData InGameData => inGameData;
    public Player Player => player;
    public PlayerInput PlayerInput => playerInput;

    private void Update() {
        if (playerInput.actions["Inventory"].WasPerformedThisFrame()) {
            if (!InventorySystem.Instance.IsOpen)
                InventorySystem.Instance.Show();
            else
                InventorySystem.Instance.Hide();
        }
    }
}
