using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    readonly int RunHash = Animator.StringToHash("Run");
    [SerializeField] float speed;
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] Animator anim;

    float playerSpriteXScale;
    Rigidbody2D rb;
    Player player;
    PlayerInput playerInput;
    Vector2 movementVector;

    InputAction movementAction;

    internal void Init(Player player) {
        this.player = player;
    }

    void Start()
    {
        playerInput = InGameManager.Instance.PlayerInput;
        if(rb == null) rb = GetComponent<Rigidbody2D>();
        playerSpriteXScale = playerSprite.transform.localScale.x;
        movementAction = playerInput.actions["Movement"];
    }

    // Update is called once per frame
    void Update() {
        movementVector = movementAction.ReadValue<Vector2>();

        anim.SetBool(RunHash, movementVector.magnitude > 0.1f);

        if (movementVector.x < 0 && playerSprite.transform.localScale.x != -playerSpriteXScale) {
            Vector3 scale = playerSprite.transform.localScale;
            scale.x = -playerSpriteXScale;
            playerSprite.transform.localScale = scale;
        }
        else if(movementVector.x > 0 && playerSprite.transform.localScale.x != playerSpriteXScale) {
            Vector3 scale = playerSprite.transform.localScale;
            scale.x = playerSpriteXScale;
            playerSprite.transform.localScale = scale;
        }else if (movementVector.x == 0) {
            FlipSprite(!player.AttacksController.FacingRight);
        }

        Movement(movementVector);
    }

    public void FlipSprite(bool facingLeft) {
        if (facingLeft && playerSprite.transform.localScale.x != -playerSpriteXScale) {
            Vector3 scale = playerSprite.transform.localScale;
            scale.x = -playerSpriteXScale;
            playerSprite.transform.localScale = scale;
        }
        else if(!facingLeft && playerSprite.transform.localScale.x != playerSpriteXScale) {
            Vector3 scale = playerSprite.transform.localScale;
            scale.x = playerSpriteXScale;
            playerSprite.transform.localScale = scale;
        }
    }

    void Movement(Vector2 direction) {
        rb.velocity = direction * speed;
    }

}
