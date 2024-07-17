using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;

    Rigidbody2D rb;
    PlayerInput playerInput;
    Vector2 movementVector;

    void Start()
    {
        playerInput = InGameManager.Instance.PlayerInput;
        if(rb == null) rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementVector = playerInput.actions["Movement"].ReadValue<Vector2>();

        Movement(movementVector);
    }

    void Movement(Vector2 direction) {
        rb.velocity = direction * speed;
    }
}
