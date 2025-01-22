using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    public static bool _isRestarting = false;
    private UIDocument _document;
    private Button _startButton;
    private Button _quitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _document = GetComponent<UIDocument>();
        _startButton = _document.rootVisualElement.Q("Start") as Button;
        _quitButton = _document.rootVisualElement.Q("Quit") as Button;

        _startButton.RegisterCallback<ClickEvent>(OnStartClick);
        _quitButton.RegisterCallback<ClickEvent>(OnQuitClick);

        if (_isRestarting)
        {
            OnStartClick(new ClickEvent());
            return;
        }
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        if(_startButton is not null)
            _startButton.UnregisterCallback<ClickEvent>(OnStartClick);
        if(_quitButton is not null)
            _quitButton.UnregisterCallback<ClickEvent>(OnQuitClick);
    }

    private void OnStartClick(ClickEvent clickEvent)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var barrier = GameObject.FindGameObjectWithTag("Barrier");

        Time.timeScale = 1.0f;
        _document.rootVisualElement.style.display = DisplayStyle.None;
        Destroy(gameObject);
    }

    private void OnQuitClick(ClickEvent clickEvent)
    {
        Application.Quit();
    }
}
