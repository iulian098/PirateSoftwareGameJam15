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
    PlayerInput PlayerInput => InGameManager.Instance.PlayerInput;

    private void Update() {

        if (GlobalData.isPaused) return;

        aimDirection = (Camera.main.ScreenToWorldPoint(PlayerInput.actions["AimDirection"].ReadValue<Vector2>()) - transform.position).normalized;
        aimDirTransform.rotation = Quaternion.LookRotation(forwardRot, aimDirection);

        if (PlayerInput.actions["Attack"].WasPressedThisFrame())
            equippedWeapon.Attack();
    }
}
