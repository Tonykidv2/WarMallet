using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuScript : MonoBehaviour
{
    private UIDocument _document;
    private Button _resumeButton;
    private Button _quitToMainMenuButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _document = GetComponent<UIDocument>();
        _resumeButton = _document.rootVisualElement.Q("Resume") as Button;
        _quitToMainMenuButton = _document.rootVisualElement.Q("QuitToMenu") as Button;

        _resumeButton.RegisterCallback<ClickEvent>(ResumeGame);
        _quitToMainMenuButton.RegisterCallback<ClickEvent>(QuitToMenu);
    }

    private void OnDisable()
    {
        if (_resumeButton != null)
            _resumeButton.UnregisterCallback<ClickEvent>(ResumeGame);
        if (_quitToMainMenuButton != null)
            _quitToMainMenuButton.UnregisterCallback<ClickEvent>(QuitToMenu);
    }

    private void ResumeGame(ClickEvent clickEvent)
    {
        Time.timeScale = 1;
        GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
    }

    private void QuitToMenu(ClickEvent clickEvent)
    {
        //Just restarts scene
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
