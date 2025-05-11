using UnityEngine;
using TMPro;
using System;

public class StatsMenuController : MonoBehaviour
{
    [SerializeField] public static StatsMenuController Instance {get; private set;}

    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text accuracyText;
    [SerializeField] private TMP_Text deathsText;
    [SerializeField] private TMP_Text levelsText;

    private PlayerScoreManager playerScoreManager;
    private GlobalStateManager globalStateManager;

    void Awake()
    {
        if (Instance) Destroy(gameObject);
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScoreManager = PlayerScoreManager.Instance;
        globalStateManager = GlobalStateManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateStats()
    {   
        int accuracy;
        if (playerScoreManager.ShotsHit < 1 || playerScoreManager.ShotsFired < 1)
        {
            accuracy = 0;
        } else
        {
            accuracy = (int) Math.Truncate((float)playerScoreManager.ShotsHit / (float)playerScoreManager.ShotsFired * 100);
        }
        
        timeText.text = Math.Truncate(globalStateManager.timeSinceStart()).ToString() + "s";
        accuracyText.text = accuracy.ToString() + "%";
        deathsText.text = playerScoreManager.Deaths.ToString();
        levelsText.text = playerScoreManager.LevelsCompleted.ToString();
    }
}
