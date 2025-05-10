using UnityEngine;

public class MenuLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private static float rotSpeed = 30;

    private LevelGenerationHandler lgh;    
    private Vector3 idolPos;
    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        lgh = gameObject.GetComponent<LevelGenerationHandler>();
        idolPos = lgh.loadMenu();
        mainCamera.transform.position = idolPos + new Vector3(4, 1.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.RotateAround(idolPos, Vector3.up, Time.deltaTime * rotSpeed);
        mainCamera.transform.LookAt(idolPos);
    }
}
