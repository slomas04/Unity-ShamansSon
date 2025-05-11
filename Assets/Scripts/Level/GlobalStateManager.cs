using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GlobalStateManager : MonoBehaviour
{
    public static GlobalStateManager Instance {get; private set;}
    public static bool LevelFinished { get; private set; }

    [SerializeField] private LevelGenerationHandler lgh;
    [SerializeField] private GameObject loadScreen;
    private bool isReloading = false;
    [SerializeField] private PlayerRotationController playerRotationController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioListener cameraListener;
    [SerializeField] private TMP_Text killsText;
    private float startTime;

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
        LevelFinished = false;
        loadPlayerSettings();
        resetInventory();
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
            if (LevelFinished)
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

    // Prepares the inventory for a new level
    private void resetInventory()
    {
        BulletSackController.Instance.newSack();
        RevolverCylinderController.Instance.resetChamberState();
        InventoryController.Instance.clearInventory();
    }

    private void handleNextLevel()
    {
        loadScreen.SetActive(true);
        LevelCompleteOverlayController.Instance.gameObject.SetActive(false);
        lgh.destroyOldLevel();
        lgh.loadLevel(PlayerScoreManager.Instance.CurrentLevel);
        isReloading = false;
        LevelFinished = false;
        resetInventory();
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
        lgh.reloadLevel();
        PlayerHealthManager.Instance.respawn();
        isReloading = false;
        startTime = Time.time;
        resetInventory();
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
        LevelFinished = true;
    }

    public TMP_Text getKillText(){
        return killsText;
    }
}
