using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float JumpStrength;
    private Rigidbody2D rb;

    private float _moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        // listening to the input manager for the key presses
        inputManager.JumpEvent += HandleJump;
        inputManager.MoveEvent += HandleMove;
        inputManager.ShootEvent += HandleShoot;
        
    }
    void OnDisable()
    {
        // stopping listening to the input manager
        inputManager.JumpEvent -= HandleJump;
        inputManager.MoveEvent -= HandleMove;
        inputManager.ShootEvent -= HandleShoot;
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(_moveDirection * MoveSpeed, rb.linearVelocityY);
    }

    private void HandleJump()
    {
        rb.linearVelocityY += JumpStrength;
    }
    private void HandleMove(float direction)
    {
        _moveDirection = direction;
    }
    private void HandleShoot()
    {

    }
}
