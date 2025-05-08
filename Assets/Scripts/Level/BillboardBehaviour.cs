using Unity.Mathematics;
using UnityEngine;

public class BillboardBehaviour : MonoBehaviour
{

    private Camera mainCamera;

    void Start()
    {
        // Get main camera
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        // Look directly at the camera
        transform.LookAt(mainCamera.transform);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
