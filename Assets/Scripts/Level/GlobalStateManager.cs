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

        pauseMenu.SetActive(false);
        settingsMenuVisible = false;

        lgh.loadLevel(1);
        loadPlayerSettings();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            if (PlayerHealthManager.Instance.IsDead && !isReloading){
                isReloading = true;
                handleRespawn();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            settingsMenuToggle();
        }
        
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
    }

    public void handleLevelLoad(){
        loadScreen.SetActive(false);
    }

    private void loadPlayerSettings(){
        mainCamera.fieldOfView = PlayerPrefs.GetFloat("Fov");
        playerRotationController.setSensitivity(PlayerPrefs.GetFloat("Sens"));
    }
}
