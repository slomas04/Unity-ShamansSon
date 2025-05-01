using UnityEngine;

public class BillboardBehaviour : MonoBehaviour
{

    private Camera mainCamera;

    void Start()
    {
        // Get main camera
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Look directly at the camera
        transform.LookAt(mainCamera.transform);
    }
}
