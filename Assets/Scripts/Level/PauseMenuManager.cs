using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        GlobalStateManager.Instance.settingsMenuToggle();
    }

    private void OnSuicideClicked()
    {
        PlayerHealthManager.Instance.killPlayer();
        GlobalStateManager.Instance.settingsMenuToggle();
    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }
    private void OnMenuClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }
}