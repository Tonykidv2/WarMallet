using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    public float Speed = 10f;
    public GameObject PauseMenu;
    public GameObject bullet;
    public Transform LaunchOffset;

    private Rigidbody2D rigidBody;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction menuAction;

    private Vector2 _moveDirection;
    private Vector2 screenBounds;

    public bool paused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions.FindAction("Attack");
        moveAction = playerInput.actions.FindAction("Move");
        menuAction = playerInput.actions.FindAction("Pause");
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        PauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        PauseMenu.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shootAction.triggered && Time.timeScale != 0 && !IsMainMenuActive())
        {
            Instantiate(bullet, LaunchOffset.position, transform.rotation);
        }

        if (!IsMainMenuActive() && menuAction.triggered)
        {
            PauseMenu.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
            Time.timeScale = 0;
        }
    }

    private void FixedUpdate()
    {
        _moveDirection = moveAction.ReadValue<Vector2>();
        rigidBody.linearVelocity = new Vector2(_moveDirection.x * Speed, _moveDirection.y * Speed);
    }

    private bool IsMainMenuActive()
    {
        return PauseMenu.GetComponent<UIDocument>().rootVisualElement.style.display == DisplayStyle.Flex;
    }
}
