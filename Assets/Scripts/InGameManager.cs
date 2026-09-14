using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InGameManager : MonoSingleton<InGameManager>
{
    [SerializeField] InGameData inGameData;
    [SerializeField] Player player;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] RecipesContainer recipesContainer;
    [SerializeField] InventoryContainer inventoryContainer;
    [SerializeField] EventSystem eventSystem;

    InputAction inventoryAction;

    public InGameData InGameData => inGameData;
    public Player Player => player;
    public PlayerInput PlayerInput => playerInput;
    public RecipesContainer RecipesContainer => recipesContainer;
    public InventoryContainer InventoryContainer => inventoryContainer;
    public EventSystem EventSystem => eventSystem;

    private void Start() {
        SaveSystem.Instance.LoadGameData();
        inventoryAction = playerInput.actions["Inventory"];
        playerInput.onActionTriggered += OnActionTriggered;
    }

    private void OnDestroy()
    {
        playerInput.onActionTriggered -= OnActionTriggered;
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
        Debug.Log(context.control.device.ToString());
    }

    private void Update() {
        if (inventoryAction.WasPerformedThisFrame()) {
            if (!InventorySystem.Instance.IsOpen)
                InventorySystem.Instance.Show();
            else
                InventorySystem.Instance.Hide();
        }
    }
}
