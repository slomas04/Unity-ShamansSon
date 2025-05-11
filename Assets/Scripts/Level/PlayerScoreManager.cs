using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class PlayerScoreManager : MonoBehaviour
{
    public static PlayerScoreManager Instance { get; private set; }

    [SerializeField] private float playTime;
    [SerializeField] private int shotsFired;
    [SerializeField] private int shotsHit;
    [SerializeField] private int maxLevel;
    [SerializeField] private int deaths;

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
            PlayTime = playTime,
            ShotsFired = shotsFired,
            ShotsHit = shotsHit,
            MaxLevel = maxLevel,
            Deaths = deaths
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
            playTime = playerData.PlayTime;
            shotsFired = playerData.ShotsFired;
            shotsHit = playerData.ShotsHit;
            maxLevel = playerData.MaxLevel;
            deaths = playerData.Deaths;

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
            MaxLevel = 0,
            Deaths = 0
        };

        SaveAllPlayerScores();
    }

    public void handleDeath(float time)
    {
        deaths++;
        playTime += time;
    }

    public void handleShotFired()
    {
        shotsFired++;
    }

    public void handleShotHit()
    {
        shotsHit++;
    }

    [System.Serializable]
    private class PlayerData
    {
        public float PlayTime;
        public int ShotsFired;
        public int ShotsHit;
        public int MaxLevel;
        public int Deaths;
    }

    [System.Serializable]
    private class PlayerScoresWrapper
    {
        public List<PlayerScoreEntry> Scores = new List<PlayerScoreEntry>();
    }

    [System.Serializable]
    private class PlayerScoreEntry
    {
        public string User;
        public PlayerData Data;
    }
}