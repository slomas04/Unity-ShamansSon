using System;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GlobalStateManager : MonoBehaviour
{
    public static GlobalStateManager Instance {get; private set;}

    [SerializeField] private LevelGenerationHandler lgh;
    [SerializeField] private GameObject loadScreen;
    private bool isReloading = false;
    [SerializeField] private PlayerRotationController playerRotationController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioListener cameraListener;
    private float startTime;
    private bool levelFinished;

    private Boolean settingsMenuVisible;

    void Awake()
    {
        if (Instance) Destroy(gameObject);
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loadScreen.SetActive(true);
        LevelCompleteOverlayController.Instance.gameObject.SetActive(false);

        pauseMenu.SetActive(false);
        settingsMenuVisible = false;

        int level = PlayerPrefs.GetInt("LevelToLoad", 1);
        lgh.loadLevel(level);
        startTime = Time.time;
        levelFinished = false;
        loadPlayerSettings();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (PlayerHealthManager.Instance.IsDead && !isReloading)
            {
                isReloading = true;
                handleRespawn();
            }
            if (levelFinished)
            {   
                isReloading = true;
                handleNextLevel();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StatsMenuController.Instance.updateStats();
            settingsMenuToggle();
        }
        
    }

    private void handleNextLevel()
    {
        loadScreen.SetActive(true);
        LevelCompleteOverlayController.Instance.gameObject.SetActive(false);
        lgh.destroyOldLevel();
        lgh.loadLevel(PlayerScoreManager.Instance.CurrentLevel);
        isReloading = false;
    }

    public void settingsMenuToggle(){
        settingsMenuVisible = !settingsMenuVisible;
        pauseMenu.SetActive(settingsMenuVisible);
        Time.timeScale = settingsMenuVisible ? 0 : 1;
        Cursor.lockState = (settingsMenuVisible) ? CursorLockMode.None : CursorLockMode.Locked;
        playerRotationController.setSettingsOpen(settingsMenuVisible);
        loadPlayerSettings();
    }

    private void handleRespawn(){
        loadScreen.SetActive(true);
        BulletSackController.Instance.resetBag();
        RevolverCylinderController.Instance.resetChamberState();
        lgh.reloadLevel();
        PlayerHealthManager.Instance.respawn();
        isReloading = false;
        startTime = Time.time;
    }

    public void handleLevelLoad(){
        loadScreen.SetActive(false);
    }

    private void loadPlayerSettings(){
        mainCamera.fieldOfView = PlayerPrefs.GetFloat("Fov");
        playerRotationController.setSensitivity(PlayerPrefs.GetFloat("Sens"));
        AudioListener.volume = PlayerPrefs.GetFloat("Volume")/100f;
    }

    public float timeSinceStart(){
        return Time.time - startTime;
    }

    public int getLivingEnemies(){
        return lgh.getLivingEnemies();
    }

    public void setLevelFinished(){
        levelFinished = true;
    }
}
