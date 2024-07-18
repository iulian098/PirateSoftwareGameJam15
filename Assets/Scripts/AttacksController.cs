using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttacksController : MonoBehaviour
{
    [SerializeField] Weapon equippedWeapon;
    [SerializeField] Transform aimDirTransform;
    [SerializeField] bool a;
    [SerializeField] Vector3 forwardRot;

    Vector2 aimDirection;
    PlayerInput PlayerInput => InGameManager.Instance.PlayerInput;

    private void Start() {
        
    }

    private void Update() {
        aimDirection = (Camera.main.ScreenToWorldPoint(PlayerInput.actions["AimDirection"].ReadValue<Vector2>()) - transform.position).normalized;
        if (a)
            aimDirTransform.rotation = Quaternion.LookRotation(forwardRot, aimDirection);
        else
            aimDirTransform.rotation = Quaternion.LookRotation(aimDirection, forwardRot);
        if (PlayerInput.actions["Attack"].WasPressedThisFrame()) {

            equippedWeapon.Attack();
        }
    }
}
