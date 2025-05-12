using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button suicideButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;

    void Start()
    {
        resumeButton.onClick.AddListener(OnResumeClicked);
        suicideButton.onClick.AddListener(OnSuicideClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
        menuButton.onClick.AddListener(OnMenuClicked);
    }

    private void OnResumeClicked()
    {
        audioSource.PlayOneShot(buttonClickSound);
        GlobalStateManager.Instance.settingsMenuToggle();
    }

    private void OnSuicideClicked()
    {
        audioSource.PlayOneShot(buttonClickSound);
        PlayerHealthManager.Instance.killPlayer();
        GlobalStateManager.Instance.settingsMenuToggle();
    }

    private void OnQuitClicked()
    {
        audioSource.PlayOneShot(buttonClickSound);
        Application.Quit();
    }
    private void OnMenuClicked()
    {
        audioSource.PlayOneShot(buttonClickSound);
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }
}