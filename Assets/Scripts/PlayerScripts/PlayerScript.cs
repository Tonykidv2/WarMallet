using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float Speed = 10f;
    public GameObject bullet;
    public Transform LaunchOffset;

    private Camera camera;
    private Rigidbody2D rigidbody;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private Vector2 _moveDirection;
    private Vector2 screenBounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        camera = Camera.main;
        screenBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(bullet, LaunchOffset.position, transform.rotation);
        }
    }


    private void FixedUpdate()
    {
        _moveDirection = moveAction.ReadValue<Vector2>();
        rigidbody.linearVelocity = new Vector2(_moveDirection.x * Speed, _moveDirection.y * Speed);
        if(_moveDirection != new Vector2(0,0))
            PreventPlayerGoingOffScreen();
    }

    private void PreventPlayerGoingOffScreen()
    {
        // Get the predicted position
        Vector3 position = rigidbody.position;

        // Calculate the GameObject's size
        float objectWidth = transform.localScale.x / 2.5f; // Adjust for sprite or collider size
        float objectHeight = transform.localScale.y / 2.5f;

        // Clamp position to screen bounds
        position.x = Mathf.Clamp(position.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        position.y = Mathf.Clamp(position.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);

        // Correct the position if necessary
        //rigidbody.MovePosition(position);

        // Optionally, stop velocity if hitting the boundary
        if (position.x <= -screenBounds.x + objectWidth || position.x >= screenBounds.x - objectWidth)
        {
            rigidbody.linearVelocity = new Vector2(position.x <= -screenBounds.x + objectWidth ? 1 : -1, rigidbody.linearVelocity.y);
        }

        if (position.y <= -screenBounds.y + objectHeight || position.y >= screenBounds.y - objectHeight)
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, position.y <= -screenBounds.y + objectHeight ? 1 : -1);
        }
    }
}
