using UnityEngine;
using System.IO;

public class LevelGenerationHandler : MonoBehaviour
{

    public static LevelGenerationHandler Instance {get; private set;}
    private static Quaternion zeroed = Quaternion.Euler(0,0,0);

    private GameObject player;
    private GameObject ceilFloor;
    private GameObject wall;

    private Vector3 spawnPos;

    void Awake()
    {
        if (Instance) Destroy(gameObject);
        Instance = this;

        ceilFloor = Resources.Load<GameObject>("Prefabs/Level/CeilFloor");
        wall = Resources.Load<GameObject>("Prefabs/Level/Wall");
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
    }

    private void handleChar(char c, int x, int y){
        Vector3 position = new Vector3(x * 4, 0, y * 4);
        GameObject created = null;
        switch(c){
            case 'T':
            case ']':
            case 'W':
                created = Instantiate(wall, position, zeroed);
                break;
            
            case '1':
            case '2':
            case '3':
            case 'L':
            case 'D':
            case 's':
            case 'm':
            case 'l':
            case 'p':
            case 'o':
            case 'b':
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
