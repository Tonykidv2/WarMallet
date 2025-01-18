using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float Speed = 10f;
    public GameObject bullet;
    public Transform LaunchOffset;

    private Rigidbody2D rigidBody;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction shootAction;
    private Vector2 _moveDirection;
    private Vector2 screenBounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions.FindAction("Attack");
        moveAction = playerInput.actions.FindAction("Move");
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if(shootAction.triggered)
        {
            Instantiate(bullet, LaunchOffset.position, transform.rotation);
        }
    }


    private void FixedUpdate()
    {
        _moveDirection = moveAction.ReadValue<Vector2>();
        rigidBody.linearVelocity = new Vector2(_moveDirection.x * Speed, _moveDirection.y * Speed);
    }
}
