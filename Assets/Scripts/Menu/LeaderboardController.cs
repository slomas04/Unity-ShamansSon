using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardPanel;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private Button usernameHeader;
    [SerializeField] private Button accuracyHeader;
    [SerializeField] private Button deathsHeader;
    [SerializeField] private Button avgTimeHeader;
    [SerializeField] private Button closeLeaderboardButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;

    private List<PlayerScoreEntry> leaderboardData;

    void Start()
    {
        usernameHeader.onClick.AddListener(() => SortLeaderboard("Username"));
        accuracyHeader.onClick.AddListener(() => SortLeaderboard("Accuracy"));
        deathsHeader.onClick.AddListener(() => SortLeaderboard("Deaths"));
        avgTimeHeader.onClick.AddListener(() => SortLeaderboard("AvgTime"));
        closeLeaderboardButton.onClick.AddListener(() => HideLeaderboard());

        LoadLeaderboardData();
        PopulateLeaderboard();
        leaderboardPanel.SetActive(false);
    }

    public void HideLeaderboard()
    {
        audioSource.PlayOneShot(buttonClickSound);
        leaderboardPanel.SetActive(false);
    }

    public void LoadLeaderboardData()
    {
        if (PlayerScoreManager.Instance == null)
        {
            leaderboardData = new List<PlayerScoreEntry>();
            return;
        }

        leaderboardData = PlayerScoreManager.Instance.GetAllPlayerScores()
            .Select(entry =>
            {
                var accuracy = entry.Data.ShotsFired > 0 
                    ? (float) entry.Data.ShotsHit / entry.Data.ShotsFired * 100 : 0;
                var avgTime = entry.Data.LevelsCompleted > 0
                    ? entry.Data.PlayTime / entry.Data.LevelsCompleted : 0;

                return new PlayerScoreEntry
                {
                    Username = entry.User,
                    Accuracy = accuracy,
                    Deaths = entry.Data.Deaths,
                    AvgTime = avgTime
                };
            })
            .ToList();
    }

    public void PopulateLeaderboard()
    {
        if (rowPrefab == null || content == null) return;

        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (var entry in leaderboardData)
        {
            var row = Instantiate(rowPrefab, content);
            var texts = row.GetComponentsInChildren<TMP_Text>();
            if (texts.Length >= 4)
            {
                texts[0].text = entry.Username;
                texts[1].text = $"{entry.Accuracy:F2}%";
                texts[2].text = $"{entry.AvgTime:F2}s";
                texts[3].text = entry.Deaths.ToString();
            }
        }
    }

    private void SortLeaderboard(string sortBy)
    {
        audioSource.PlayOneShot(buttonClickSound);
        if (leaderboardData == null || leaderboardData.Count == 0) return;

        switch (sortBy)
        {
            case "Username":
                leaderboardData = leaderboardData.OrderBy(e => e.Username).ToList();
                break;
            case "Accuracy":
                leaderboardData = leaderboardData.OrderByDescending(e => e.Accuracy).ToList();
                break;
            case "Deaths":
                leaderboardData = leaderboardData.OrderBy(e => e.Deaths).ToList();
                break;
            case "AvgTime":
                leaderboardData = leaderboardData.OrderBy(e => e.AvgTime).ToList();
                break;
        }

        PopulateLeaderboard();
    }

    [System.Serializable]
    public class PlayerScoreEntry
    {
        public string Username;
        public float Accuracy;
        public int Deaths;
        public float AvgTime;
    }
}