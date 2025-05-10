using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button suicideButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button quitButton;

    void Start()
    {
        resumeButton.onClick.AddListener(OnResumeClicked);
        suicideButton.onClick.AddListener(OnSuicideClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
        menuButton.onClick.AddListener(OnMenuClicked);
    }

    private void OnResumeClicked()
    {
        Debug.Log("Resume button clicked");
    }

    private void OnSuicideClicked()
    {
        Debug.Log("Settings button clicked");
    }

    private void OnQuitClicked()
    {
        Debug.Log("Quit button clicked");
    }
    private void OnMenuClicked()
    {
        Debug.Log("Menu button clicked");
    }
}