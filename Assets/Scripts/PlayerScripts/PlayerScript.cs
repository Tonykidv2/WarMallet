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
        //if(_moveDirection != new Vector2(0,0))
        //    PreventPlayerGoingOffScreen();
    }

    private void PreventPlayerGoingOffScreen()
    {
        // Get the predicted position
        Vector3 position = rigidBody.position;

        // Calculate the GameObject's size
        float objectWidth = transform.localScale.x / 2.5f; // Adjust for sprite or collider size
        float objectHeight = transform.localScale.y / 2.5f;

        // Clamp position to screen bounds
        position.x = Mathf.Clamp(position.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        position.y = Mathf.Clamp(position.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);

        // Optionally, stop velocity if hitting the boundary
        if (position.x <= -screenBounds.x + objectWidth || position.x >= screenBounds.x - objectWidth)
        {
            rigidBody.linearVelocity = 
                new Vector2(position.x <= -screenBounds.x + objectWidth ? 1 : -1, rigidBody.linearVelocity.y);
        }
        if (position.y <= -screenBounds.y + objectHeight || position.y >= screenBounds.y - objectHeight)
        {
            rigidBody.linearVelocity = 
                new Vector2(rigidBody.linearVelocity.x, position.y <= -screenBounds.y + objectHeight ? -1 : 1);
        }
    }
}
