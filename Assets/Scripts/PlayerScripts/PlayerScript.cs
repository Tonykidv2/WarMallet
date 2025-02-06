using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    public float Speed = 10f;
    public GameObject PauseMenu;
    public GameObject GameOver;
    public GameObject bullet;
    public Transform LaunchOffset;

    public HealthManager HealthManager;

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

        HealthManager = GameObject.FindGameObjectWithTag("Healthbar").GetComponent<HealthManager>();

        PauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        PauseMenu.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        GameOver = GameObject.FindGameObjectWithTag("GameOver");
        GameOver.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
    }

    // Update is called once per frame
    void Update()
    {
        if(HealthManager.GetHealth() <= 0)
        {
            Time.timeScale = 0;
            GameOver.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
            var label = GameOver.GetComponent<UIDocument>().rootVisualElement.Q<Label>("FinalScoreNumber");
            label.text = GameObject.FindGameObjectWithTag("Barrier").GetComponent<BarrierObject>().GetScore().ToString();
        }

        if(shootAction.triggered && Time.timeScale != 0 && !IsMainMenuActive())
        {
            Instantiate(bullet, LaunchOffset.position, transform.rotation);
        }
        if (!IsMainMenuActive() && menuAction.triggered)
        {
            PauseMenu.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
            Time.timeScale = 0;
        }
        else if (IsMainMenuActive() && menuAction.triggered)
        {
            PauseMenu.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
            Time.timeScale = 1;
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

    public void TakenDamage(string enemyName)
    {
        HealthManager.TakeDamage(10);
    }
}
