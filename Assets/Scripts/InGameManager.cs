using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameManager : MonoBehaviour
{
    #region Singleton

    public static InGameManager Instance;

    private void Awake() {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    #endregion

    [SerializeField] PlayerInput playerInput;

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
