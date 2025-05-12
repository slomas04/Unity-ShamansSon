using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class PlayerScoreManager : MonoBehaviour
{
    public static PlayerScoreManager Instance { get; private set; }

    [SerializeField] public float PlayTime { get; private set; }
    [SerializeField] public int ShotsFired { get; private set; }
    [SerializeField] public int ShotsHit { get; private set; }
    [SerializeField] public int LevelsCompleted{ get; private set; }
    [SerializeField] public int CurrentLevel { get; private set; }
    [SerializeField] public int Deaths { get; private set; }

    private string currentPlayer;
    private Dictionary<string, PlayerData> allPlayerScores = new Dictionary<string, PlayerData>();

    void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this;
    }

    private void Start()
    {
        InitializePlayer();
        Debug.Log($"Persistant Data Path: {Application.persistentDataPath}");
    }

    public void InitializePlayer()
    {
        currentPlayer = PlayerPrefs.GetString("currentUser");

        if (string.IsNullOrEmpty(currentPlayer)) return;

        LoadAllPlayerScores();

        if (!allPlayerScores.ContainsKey(currentPlayer))
        {
            CreateNewUserScore(currentPlayer);
        }
        else
        {
            LoadCurrentPlayerScores();
        }
    }

    public void SaveCurrentPlayerScores()
    {
        PlayerData playerData = new PlayerData
        {
            PlayTime = PlayTime,
            ShotsFired = ShotsFired,
            ShotsHit = ShotsHit,
            LevelsCompleted = LevelsCompleted,
            CurrentLevel = CurrentLevel,
            Deaths = Deaths
        };

        allPlayerScores[currentPlayer] = playerData;
        SaveAllPlayerScores();
    }

    private void SaveAllPlayerScores()
    {
        string filePath = Application.persistentDataPath + "/PlayerScores.json";

        // Serialize the player data to JSON
        PlayerScoresWrapper wrapper = new PlayerScoresWrapper();
        foreach (var entry in allPlayerScores)
        {
            wrapper.Scores.Add(new PlayerScoreEntry { User = entry.Key, Data = entry.Value });
        }

        string json = JsonUtility.ToJson(wrapper, true);

        // Write the JSON to file
        File.WriteAllText(filePath, json);
    }

    private void LoadAllPlayerScores()
    {
        string filePath = Application.persistentDataPath + "/PlayerScores.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            // Deserialize the JSON data
            PlayerScoresWrapper wrapper = JsonUtility.FromJson<PlayerScoresWrapper>(json);
            allPlayerScores = new Dictionary<string, PlayerData>();

            if (wrapper != null && wrapper.Scores != null)
            {
                foreach (var entry in wrapper.Scores)
                {
                    allPlayerScores[entry.User] = entry.Data;
                }
            }
        } else
        {   
            // Create an empty file if it doesn't exist
            SaveAllPlayerScores();
        }
    }

    private void LoadCurrentPlayerScores()
    {
        if (allPlayerScores.TryGetValue(currentPlayer, out PlayerData playerData))
        {
            PlayTime = playerData.PlayTime;
            ShotsFired = playerData.ShotsFired;
            ShotsHit = playerData.ShotsHit;
            LevelsCompleted = playerData.LevelsCompleted;
            CurrentLevel = playerData.CurrentLevel;
            Deaths = playerData.Deaths;

        }
    }

    public void CreateNewUserScore(string newUsername)
    {
        if (allPlayerScores.ContainsKey(newUsername)) return;

        allPlayerScores[newUsername] = new PlayerData
        {
            PlayTime = 0,
            ShotsFired = 0,
            ShotsHit = 0,
            LevelsCompleted = 0,
            CurrentLevel = 1,
            Deaths = 0
        };

        SaveAllPlayerScores();
    }

    public void handleDeath(float time)
    {
        Deaths++;
        PlayTime += time;
    }

    public void handleShotFired()
    {
        ShotsFired++;
    }

    public void handleShotHit()
    {
        ShotsHit++;
    }

    public void handleStartNewGame()
    {
        CurrentLevel = 1;
        SaveCurrentPlayerScores();
    }

    public void completeLevel()
    {
        LevelsCompleted++;
        PlayTime += Time.time;
        CurrentLevel++;
        if (CurrentLevel > 5) {
            CurrentLevel = 1; 
            SaveCurrentPlayerScores();
            SceneManager.LoadScene("MenuScene");
            return;
        }
        SaveCurrentPlayerScores();
    }

    public List<PlayerScoreEntry> GetAllPlayerScores()
    {
        var playerScoreEntries = new List<PlayerScoreEntry>();

        foreach (var entry in allPlayerScores)
        {
            playerScoreEntries.Add(new PlayerScoreEntry
            {
                User = entry.Key,
                Data = entry.Value
            });
        }

        return playerScoreEntries;
    }

    [System.Serializable]
    public class PlayerData
    {
        public float PlayTime;
        public int ShotsFired;
        public int ShotsHit;
        public int LevelsCompleted;
        public int CurrentLevel;
        public int Deaths;
    }

    [System.Serializable]
    private class PlayerScoresWrapper
    {
        public List<PlayerScoreEntry> Scores = new List<PlayerScoreEntry>();
    }

    [System.Serializable]
    public class PlayerScoreEntry
    {
        public string User;
        public PlayerData Data;
    }
}