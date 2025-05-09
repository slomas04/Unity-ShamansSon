using UnityEngine;
using UnityEngine.Assertions.Must;

public class GlobalStateManager : MonoBehaviour
{
    public static GlobalStateManager Instance {get; private set;}

    private LevelGenerationHandler lgh;    
    private GameObject loadScreen;
    private bool isReloading = false;

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
        lgh = gameObject.GetComponent<LevelGenerationHandler>();
        lgh.loadLevel(1);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            if (PlayerHealthManager.Instance.IsDead && !isReloading){
                isReloading = true;
                handleRespawn();
            }
        }
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
}
