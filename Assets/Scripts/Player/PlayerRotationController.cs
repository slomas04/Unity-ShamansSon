using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    private float yaw = 0f;
    private float pitch = 0f;

    private bool lookLock = false;

    public float sensitivity = 7.5f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealthManager.Instance.IsDead) return;
        if (lookLock)
        {
            return;
        }
        // Pitch must be inverted, then clamp it
        pitch -= Input.GetAxisRaw("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        yaw += Input.GetAxisRaw("Mouse X") * sensitivity;

        // Set pitch for camera, yaw for object
        Camera.main.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        gameObject.transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }

    void toggleInventoryShow(bool show)
    {
        lookLock = show;
        Cursor.lockState = (show) ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
