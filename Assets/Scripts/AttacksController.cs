using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttacksController : MonoBehaviour
{
    [SerializeField] Weapon equippedWeapon;
    [SerializeField] Transform aimDirTransform;
    [SerializeField] Vector3 forwardRot;

    Vector2 aimDirection;
    InputAction attackAction;
    InputAction aimDirectionAction;
    PlayerInput PlayerInput => InGameManager.Instance.PlayerInput;

    private void Start() {
        attackAction = PlayerInput.actions["Attack"];
        aimDirectionAction = PlayerInput.actions["AimDirection"];
    }

    private void Update() {

        if (GlobalData.isPaused) return;
        if (InGameManager.Instance.EventSystem.IsPointerOverGameObject()) return;

        aimDirection = (Camera.main.ScreenToWorldPoint(aimDirectionAction.ReadValue<Vector2>()) - transform.position).normalized;
        aimDirTransform.rotation = Quaternion.LookRotation(forwardRot, aimDirection);

        if (attackAction.WasPressedThisFrame())
            equippedWeapon.Attack();
    }
}
