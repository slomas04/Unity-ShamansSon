using System;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GlobalStateManager : MonoBehaviour
{
    public static GlobalStateManager Instance {get; private set;}

    private LevelGenerationHandler lgh;
    private GameObject loadScreen;
    private bool isReloading = false;
    private PlayerRotationController playerRotationController;
    private Camera mainCamera;
    private GameObject settingsMenu;

    private Boolean settingsMenuVisible;

    void Awake()
    {
        if (Instance) Destroy(gameObject);
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loadScreen = GameObject.Find("LoadCanvas");
        loadScreen.SetActive(true);

        settingsMenu = GameObject.Find("PauseMenu");
        settingsMenu.SetActive(false);
        settingsMenuVisible = false;

        lgh = gameObject.GetComponent<LevelGenerationHandler>();
        lgh.loadLevel(1);
        mainCamera = Camera.main;
        playerRotationController = GameObject.Find("Player").GetComponent<PlayerRotationController>();
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
        settingsMenu.SetActive(settingsMenuVisible);
        Time.timeScale = settingsMenuVisible ? 0 : 1;
        Cursor.lockState = (settingsMenuVisible) ? CursorLockMode.None : CursorLockMode.Locked;
        playerRotationController.setSettingsOpen(settingsMenuVisible);
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
