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

    private float previousAvg;
    private float previousAccuracy;
    private int previousDeaths;

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

    public void loadStartingValues(){
        previousAccuracy = getAccuracy();
        previousAvg = getAvgTime();
        previousDeaths = playerScoreManager.Deaths;
    }

    private float getAccuracy(){
        int accuracy;
        if (playerScoreManager.ShotsHit < 1 || playerScoreManager.ShotsFired < 1)
        {
            accuracy = 0;
        } else
        {
            accuracy = (int) Math.Truncate((float)playerScoreManager.ShotsHit / (float)playerScoreManager.ShotsFired * 100);
        }
        return accuracy;
    }

    private float getAvgTime()
    {
        return (float) Math.Truncate((playerScoreManager.PlayTime / playerScoreManager.LevelsCompleted) * 100)/100f;
    }

    public void updateText()
    {
        timeText.text = globalStateManager.timeSinceStart().ToString() + "s";

        float currentAvg = getAvgTime();
        int currentAccuracy = (int)getAccuracy();
        int currentDeaths = playerScoreManager.Deaths;

        // Set the text for avg time, accuracy and deaths. If there is an improvement in scores the text is green, if there is a worsening it is red.
        avgText.text = currentAvg.ToString() + "s";
        avgText.color = currentAvg < previousAvg ? Color.green : (currentAvg > previousAvg ? Color.red : Color.white);

        accuracyText.text = currentAccuracy.ToString() + "%";
        accuracyText.color = currentAccuracy > previousAccuracy ? Color.green : (currentAccuracy < previousAccuracy ? Color.red : Color.white);

        deathsText.text = currentDeaths.ToString();
        deathsText.color = currentDeaths < previousDeaths ? Color.green : (currentDeaths > previousDeaths ? Color.red : Color.white);
    }
}
