using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class LevelCompleteOverlayController : MonoBehaviour
{
    public static LevelCompleteOverlayController Instance { get; private set; }

    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text avgText;
    [SerializeField] private TMP_Text accuracyText;
    [SerializeField] private TMP_Text deathsText;

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

    public void updateText()
    {
        int accuracy;
        if (playerScoreManager.ShotsHit < 1 || playerScoreManager.ShotsFired < 1)
        {
            accuracy = 0;
        } else
        {
            accuracy = (int) Math.Truncate((float)playerScoreManager.ShotsHit / (float)playerScoreManager.ShotsFired * 100);
        }
        float avgTime = (float) Math.Truncate((playerScoreManager.PlayTime / playerScoreManager.LevelsCompleted) * 100)/100f;

        timeText.text = globalStateManager.timeSinceStart().ToString() + "s";
        avgText.text = avgTime.ToString() + "s";
        accuracyText.text = accuracy.ToString() + "%";
        deathsText.text = playerScoreManager.Deaths.ToString();
    }
}
