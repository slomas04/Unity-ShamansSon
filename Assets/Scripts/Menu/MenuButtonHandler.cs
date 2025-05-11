using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MenuButtonHandler : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private LeaderboardController leaderboardController;

    [SerializeField] private GameObject settingsMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingsMenu.SetActive(false);

        exitButton.onClick.AddListener(() => Application.Quit());
        settingsButton.onClick.AddListener(() => settingsMenu.SetActive(true));
        leaderboardButton.onClick.AddListener(() => showLeaderboard());
        newGameButton.onClick.AddListener(() => StartNewGame());
        continueButton.onClick.AddListener(() => ContinueGame());
    }

    private void showLeaderboard()
    {
        leaderboardController.gameObject.SetActive(true);
        leaderboardController.LoadLeaderboardData();
        leaderboardController.PopulateLeaderboard();
    }
    private void StartNewGame()
    {
        PlayerScoreManager.Instance.handleStartNewGame();
        PlayerPrefs.SetInt("LevelToLoad", 1);
        SceneManager.LoadScene("GameScene");
    }

    private void ContinueGame()
    {
        int lastLevel = PlayerScoreManager.Instance.CurrentLevel;
        PlayerPrefs.SetInt("LevelToLoad", lastLevel);
        SceneManager.LoadScene("GameScene");
    }
}
