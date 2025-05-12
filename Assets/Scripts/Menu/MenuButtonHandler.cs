using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MenuButtonHandler : MonoBehaviour
{
    public static bool TabOpen { get; set; }
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private TutorialMenuController tutorialMenuController;
    [SerializeField] private LeaderboardController leaderboardController;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;

    [SerializeField] private GameObject settingsMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingsMenu.SetActive(false);

        exitButton.onClick.AddListener(() => Application.Quit());
        settingsButton.onClick.AddListener(() => showSettingsMenu());
        leaderboardButton.onClick.AddListener(() => showLeaderboard());
        newGameButton.onClick.AddListener(() => StartNewGame());
        continueButton.onClick.AddListener(() => ContinueGame());
        tutorialButton.onClick.AddListener(() => showTutorialMenu());
    }

    private void showSettingsMenu()
    {   
        if (TabOpen) return;
        TabOpen = true;
        audioSource.PlayOneShot(buttonClickSound);
        settingsMenu.SetActive(true);
    }

    private void showTutorialMenu()
    {
        if (TabOpen) return;
        TabOpen = true;
        audioSource.PlayOneShot(buttonClickSound);
        tutorialMenuController.gameObject.SetActive(true);
    }

    private void showLeaderboard()
    {
        if (TabOpen) return;
        TabOpen = true;
        audioSource.PlayOneShot(buttonClickSound);
        leaderboardController.gameObject.SetActive(true);
        leaderboardController.LoadLeaderboardData();
        leaderboardController.PopulateLeaderboard();
    }
    private void StartNewGame()
    {
        if (TabOpen) return;
        audioSource.PlayOneShot(buttonClickSound);
        PlayerScoreManager.Instance.handleStartNewGame();
        PlayerPrefs.SetInt("LevelToLoad", 1);
        SceneManager.LoadScene("GameScene");
    }

    private void ContinueGame()
    {
        if (TabOpen) return;
        audioSource.PlayOneShot(buttonClickSound);
        int lastLevel = PlayerScoreManager.Instance.CurrentLevel;
        PlayerPrefs.SetInt("LevelToLoad", lastLevel);
        SceneManager.LoadScene("GameScene");
    }
}
