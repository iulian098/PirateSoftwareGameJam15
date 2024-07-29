using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttacksController : MonoBehaviour
{
    [SerializeField] Weapon equippedWeapon;
    [SerializeField] Transform aimDirTransform;
    [SerializeField] Vector3 forwardRot;

    float dotProduct;
    Vector2 aimDirection;
    InputAction attackAction;
    InputAction aimDirectionAction;
    PlayerInput PlayerInput => InGameManager.Instance.PlayerInput;
    public bool FacingRight => dotProduct > 0;

    private void Start() {
        attackAction = PlayerInput.actions["Attack"];
        aimDirectionAction = PlayerInput.actions["AimDirection"];
    }

    private void Update() {

        if (GlobalData.isPaused) return;
        if (InGameManager.Instance.EventSystem.IsPointerOverGameObject()) return;

        aimDirection = (Camera.main.ScreenToWorldPoint(aimDirectionAction.ReadValue<Vector2>()) - transform.position).normalized;
        aimDirTransform.rotation = Quaternion.LookRotation(forwardRot, aimDirection);

        dotProduct = Vector3.Dot(Vector2.right, aimDirection);

        if (attackAction.WasPressedThisFrame()) {
            equippedWeapon.Attack();

        }
    }
}
