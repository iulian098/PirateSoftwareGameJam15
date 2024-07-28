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
        inventoryAction = playerInput.actions["Inventory"];
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
