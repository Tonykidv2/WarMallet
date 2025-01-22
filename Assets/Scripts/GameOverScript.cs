using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverScript : MonoBehaviour
{
    private UIDocument _document;
    private Button _restartButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _document = GetComponent<UIDocument>();
        _restartButton = _document.rootVisualElement.Q("Restart") as Button;

        _restartButton.RegisterCallback<ClickEvent>(QuitToMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void QuitToMenu(ClickEvent clickEvent)
    {
        //Just restarts scene
        Scene scene = SceneManager.GetActiveScene();
        MainMenuEvents._isRestarting = true;
        SceneManager.LoadScene(scene.name);
    }
}
