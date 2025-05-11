using UnityEngine;
using System.IO;
using UnityEditor;
using System.Collections.Generic;

public class LevelGenerationHandler : MonoBehaviour
{

    public static LevelGenerationHandler Instance {get; private set;}
    private static Quaternion zeroed = Quaternion.Euler(0,0,0);

    private List<DoorController> doors;
    private List<GameObject> entities;
    private List<EnemyStateController> enemies;

    private GameObject player;
    private GameObject ceilFloor;
    private GameObject itemPickupPrefab;
    private GameObject wall;
    private GameObject wallTorch;
    private GameObject lampFloor;
    private GameObject exitDoors;
    private GameObject door;
    private GameObject snakeEnemy;
    private GameObject skeletonEnemy;
    private GameObject idol;

    private Vector3 spawnPos;
    private Vector3 idolPos;

    private int lastLoadedLevel;

    void Awake()
    {
        if (Instance) Destroy(gameObject);
        Instance = this;

        doors = new List<DoorController>();
        entities = new List<GameObject>();
        enemies = new List<EnemyStateController>();

        ceilFloor = Resources.Load<GameObject>("Prefabs/Level/CeilFloor");
        wall = Resources.Load<GameObject>("Prefabs/Level/Wall");
        wallTorch = Resources.Load<GameObject>("Prefabs/Level/WallTorch");
        lampFloor = Resources.Load<GameObject>("Prefabs/Level/LampFloor");
        itemPickupPrefab = Resources.Load<GameObject>("Prefabs/ItemPickup");
        exitDoors = Resources.Load<GameObject>("Prefabs/Level/ExitDoors");
        idol = Resources.Load<GameObject>("Prefabs/Level/TheIdol");
        door = Resources.Load<GameObject>("Prefabs/Level/Door");

        snakeEnemy = Resources.Load<GameObject>("Prefabs/Enemy/SnakeEnemy");
        skeletonEnemy = Resources.Load<GameObject>("Prefabs/Enemy/SkeletonEnemy");

    }

    void Start()
    {
        
    }

    public Vector3 loadMenu(){
        loadLevelFromFile("M");
        return idolPos;
    }

    public void loadLevel(int levelNo){
        lastLoadedLevel = levelNo;
        player = GameObject.Find("Player");
        player.SetActive(false);
        loadLevelFromFile(levelNo.ToString());
        if(spawnPos != null){
            Debug.Log(spawnPos);
            player.transform.SetPositionAndRotation(spawnPos + new Vector3(0,1.75f,0), zeroed);
            player.SetActive(true);
        } else {
            Debug.Log("No Spawn position given!");
        }

        GlobalStateManager.Instance.handleLevelLoad();
    }

    void Update()
    {
        
    }

    private void loadLevelFromFile(string levelNo){
        TextAsset leveldata = Resources.Load<TextAsset>("Levels/Level" + levelNo);
        if (leveldata == null){
            Debug.Log("Unable to find level " + levelNo + "!");
            return;
        }
        string textData = leveldata.text;

        using (StringReader reader = new StringReader(textData)){
            string line = reader.ReadLine();
            int lineNo = 0;
            while(line != null){
                for(int i = 0; i < line.Length; i++){
                    handleChar(line[i], i, lineNo);
                }

                line = reader.ReadLine();
                lineNo++;   
            }
        }

        // Get all torches and cull them if they are not above ground
        foreach(GameObject torch in GameObject.FindGameObjectsWithTag("Torch")){
            if(!Physics.Raycast(torch.transform.position, Vector3.down, 10)) {
                GameObject.Destroy(torch);
            }
        }

        // Set proper rotation for each door
        foreach (DoorController d in doors){
            d.castRotation();
        }
    }

    private void handleChar(char c, int x, int y){
        Vector3 position = new Vector3(x * 4, 0, y * 4);
        GameObject created = null;
        GameObject ent = null;
        switch(c){
            case 'T':
                created = Instantiate(wallTorch, position, zeroed);
                break;

            case ']':
                created = Instantiate(exitDoors, position, zeroed);
                break;

            case 'W':
                created = Instantiate(wall, position, zeroed);
                break;

            case 'p':
                created = Instantiate(ceilFloor, position, zeroed);
                ent = Instantiate(itemPickupPrefab, position + new Vector3(0,1,0), zeroed);
                ent.GetComponent<PhysicsItemBehaviour>().setContainedItem(new Gunpowder());
                entities.Add(ent);
                break;

            case 'o':
                created = Instantiate(ceilFloor, position, zeroed);
                ent = Instantiate(itemPickupPrefab, position + new Vector3(0,1,0), zeroed);
                ent.GetComponent<PhysicsItemBehaviour>().setContainedItem(new Primer());
                entities.Add(ent);
                break;

            case 'b':
                created = Instantiate(ceilFloor, position, zeroed);
                ent = Instantiate(itemPickupPrefab, position + new Vector3(0,1,0), zeroed);
                ent.GetComponent<PhysicsItemBehaviour>().setContainedItem(new Bullet());
                entities.Add(ent);
                break;

            case 's':
                created = Instantiate(ceilFloor, position, zeroed);
                ent = Instantiate(itemPickupPrefab, position + new Vector3(0,1,0), zeroed);
                ent.GetComponent<PhysicsItemBehaviour>().setContainedItem(new ItemCasing(ItemCasing.CASING_SIZE.SMALL));
                entities.Add(ent);
                break;

            case 'm':
                created = Instantiate(ceilFloor, position, zeroed);
                ent = Instantiate(itemPickupPrefab, position + new Vector3(0,1,0), zeroed);
                ent.GetComponent<PhysicsItemBehaviour>().setContainedItem(new ItemCasing(ItemCasing.CASING_SIZE.MEDIUM));
                entities.Add(ent);
                break;

            case 'l':
                created = Instantiate(ceilFloor, position, zeroed);
                ent = Instantiate(itemPickupPrefab, position + new Vector3(0,1,0), zeroed);
                ent.GetComponent<PhysicsItemBehaviour>().setContainedItem(new ItemCasing(ItemCasing.CASING_SIZE.LARGE));
                entities.Add(ent);
                break;

            
            case '1':
                created = Instantiate(ceilFloor, position, zeroed);
                ent = Instantiate(snakeEnemy, position + new Vector3(0,0,0), zeroed);
                entities.Add(ent);
                enemies.Add(ent.GetComponent<EnemyStateController>());
                break;
            case '2':
                created = Instantiate(ceilFloor, position, zeroed);
                ent = Instantiate(skeletonEnemy, position + new Vector3(0,0,0), zeroed);
                entities.Add(ent);
                enemies.Add(ent.GetComponent<EnemyStateController>());
                break;
            case 'L':
                created = Instantiate(lampFloor, position, zeroed);
                break;
                
            case 'D':
                created = Instantiate(door, position, zeroed);
                doors.Add(created.GetComponentInChildren<DoorController>());
                break;

            case '-':
                created = Instantiate(ceilFloor, position, zeroed);
                break;

            case '[':
                created = Instantiate(ceilFloor, position, zeroed);
                spawnPos = position;
                break;

            case 'G':
                created = Instantiate(idol, position, zeroed);
                idolPos = position + new Vector3(0,1,0);
                break;
        }

        if(created != null){
            created.transform.parent = transform;
        }
    }

    public void reloadLevel(){
        destroyOldLevel();
        loadLevel(lastLoadedLevel);
    }

    public void destroyOldLevel(){
        foreach (GameObject e in entities){
            Destroy(e);
        }
        entities = new List<GameObject>();
        enemies = new List<EnemyStateController>();
        doors = new List<DoorController>();
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform t in children) {
            if (t != this.transform) Destroy(t.gameObject);
        }
    }

    public int getLivingEnemies(){
        int count = 0;
        foreach (EnemyStateController e in enemies){
            if(!e.isDead()){
                count++;
            }
        }
        return count;
    }
}
