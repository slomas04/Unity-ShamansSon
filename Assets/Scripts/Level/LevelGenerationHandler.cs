using UnityEngine;
using System.IO;
using UnityEditor;

public class LevelGenerationHandler : MonoBehaviour
{

    public static LevelGenerationHandler Instance {get; private set;}
    private static Quaternion zeroed = Quaternion.Euler(0,0,0);

    private GameObject player;
    private GameObject ceilFloor;
    private GameObject itemPickupPrefab;
    private GameObject wall;
    private GameObject wallTorch;
    private GameObject lampFloor;

    private Vector3 spawnPos;

    void Awake()
    {
        if (Instance) Destroy(gameObject);
        Instance = this;

        ceilFloor = Resources.Load<GameObject>("Prefabs/Level/CeilFloor");
        wall = Resources.Load<GameObject>("Prefabs/Level/Wall");
        wallTorch = Resources.Load<GameObject>("Prefabs/Level/WallTorch");
        lampFloor = Resources.Load<GameObject>("Prefabs/Level/LampFloor");
        itemPickupPrefab = Resources.Load<GameObject>("Prefabs/ItemPickup");

    }

    void Start()
    {
        player = GameObject.Find("Player");
        player.SetActive(false);
        loadLevelFromFile(1);
        if(spawnPos != null){
            Debug.Log(spawnPos);
            player.transform.SetPositionAndRotation(spawnPos + new Vector3(0,1,0), zeroed);
            player.SetActive(true);
        } else {
            Debug.Log("No Spawn position given!");
        }
    }

    void Update()
    {
        
    }

    private void loadLevelFromFile(int levelNo){
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
    }

    private void handleChar(char c, int x, int y){
        Vector3 position = new Vector3(x * 4, 0, y * 4);
        GameObject created = null;
        switch(c){
            case 'T':
                created = Instantiate(wallTorch, position, zeroed);
                break;

            case ']':
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
            case '2':
            case '3':
            case 'L':
                created = Instantiate(lampFloor, position, zeroed);
                break;
                
            case 'D':
            case '-':
                created = Instantiate(ceilFloor, position, zeroed);
                break;

            case '[':
                created = Instantiate(ceilFloor, position, zeroed);
                spawnPos = position;
                break;
        }

        if(created != null){
            created.transform.parent = transform;
        }
    }

}
