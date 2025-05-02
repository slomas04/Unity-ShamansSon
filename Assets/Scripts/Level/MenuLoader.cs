using UnityEngine;

public class MenuLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private LevelGenerationHandler lgh;    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lgh = gameObject.GetComponent<LevelGenerationHandler>();
        lgh.loadMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
