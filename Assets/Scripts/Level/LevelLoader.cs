using UnityEngine;

public class LevelLoader : MonoBehaviour
{

    private LevelGenerationHandler lgh;    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lgh = gameObject.GetComponent<LevelGenerationHandler>();
        lgh.loadLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
