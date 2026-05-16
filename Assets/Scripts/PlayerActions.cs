using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float JumpStrength;
    [SerializeField] private float raycastStartDown;
    [SerializeField] private float raycastDistance;
    [SerializeField] private int _hp;
    private Rigidbody2D rb;

    private float _moveDirection;
    private bool _alive = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(LayerMask.GetMask("Player"));
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
        if (_alive)
        {
            rb.linearVelocity = new Vector2(_moveDirection * MoveSpeed, rb.linearVelocityY);
        }
    }

    private void HandleJump()
    {
        if (IsGrounded() && _alive)
        {
            rb.linearVelocityY += JumpStrength;
        }
    }
    private void HandleMove(float direction)
    {
        _moveDirection = direction;
    }
    private void HandleShoot()
    {

    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position + raycastStartDown * Vector3.down, Vector2.down, raycastDistance, LayerMask.GetMask("Ground"));
    }
    public void TakeDamage(int damage)
    {
        _hp -= damage;
        Debug.Log("Hit for " + damage);
        if (_hp <= 0)
        {
            _alive = false;
        }
    }
}
