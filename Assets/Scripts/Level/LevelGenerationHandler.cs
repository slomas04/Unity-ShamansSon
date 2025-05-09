using UnityEngine;
using System.IO;
using UnityEditor;
using System.Collections.Generic;

public class LevelGenerationHandler : MonoBehaviour
{

    public static LevelGenerationHandler Instance {get; private set;}
    private static Quaternion zeroed = Quaternion.Euler(0,0,0);

    private List<DoorController> doors;

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

    void Awake()
    {
        if (Instance) Destroy(gameObject);
        Instance = this;

        doors = new List<DoorController>();

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
                Instantiate(itemPickupPrefab, position + new Vector3(0,1,0), zeroed)
                    .GetComponent<PhysicsItemBehaviour>().setContainedItem(new Gunpowder());
                break;

            case 'o':
                created = Instantiate(ceilFloor, position, zeroed);
                Instantiate(itemPickupPrefab, position + new Vector3(0,1,0), zeroed)
                    .GetComponent<PhysicsItemBehaviour>().setContainedItem(new Primer());
                break;

            case 'b':
                created = Instantiate(ceilFloor, position, zeroed);
                Instantiate(itemPickupPrefab, position + new Vector3(0,1,0), zeroed)
                    .GetComponent<PhysicsItemBehaviour>().setContainedItem(new Bullet());
                break;

            case 's':
                created = Instantiate(ceilFloor, position, zeroed);
                Instantiate(itemPickupPrefab, position + new Vector3(0,1,0), zeroed)
                    .GetComponent<PhysicsItemBehaviour>().setContainedItem(new ItemCasing(ItemCasing.CASING_SIZE.SMALL));
                break;

            case 'm':
                created = Instantiate(ceilFloor, position, zeroed);
                Instantiate(itemPickupPrefab, position + new Vector3(0,1,0), zeroed)
                    .GetComponent<PhysicsItemBehaviour>().setContainedItem(new ItemCasing(ItemCasing.CASING_SIZE.MEDIUM));
                break;

            case 'l':
                created = Instantiate(ceilFloor, position, zeroed);
                Instantiate(itemPickupPrefab, position + new Vector3(0,1,0), zeroed)
                    .GetComponent<PhysicsItemBehaviour>().setContainedItem(new ItemCasing(ItemCasing.CASING_SIZE.LARGE));
                break;

            
            case '1':
                created = Instantiate(ceilFloor, position, zeroed);
                Instantiate(snakeEnemy, position + new Vector3(0,0,0), zeroed);
                break;
            case '2':
                created = Instantiate(ceilFloor, position, zeroed);
                Instantiate(skeletonEnemy, position + new Vector3(0,0,0), zeroed);
                break;
            case '3':
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

}
