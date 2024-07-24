using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    readonly int RunHash = Animator.StringToHash("Run");
    [SerializeField] float speed;
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] Animator anim;
    float playerSpriteXScale;
    Rigidbody2D rb;
    PlayerInput playerInput;
    Vector2 movementVector;

    void Start()
    {
        playerInput = InGameManager.Instance.PlayerInput;
        if(rb == null) rb = GetComponent<Rigidbody2D>();
        playerSpriteXScale = playerSprite.transform.localScale.x;
    }

    // Update is called once per frame
    void Update() {
        movementVector = playerInput.actions["Movement"].ReadValue<Vector2>();

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
        }

        Movement(movementVector);
    }

    void Movement(Vector2 direction) {
        rb.velocity = direction * speed;
    }
}
