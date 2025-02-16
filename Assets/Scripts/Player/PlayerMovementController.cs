using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rigidBodyComponent; 

    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputAzi = Input.GetAxis("Horizontal");
        float inputAlt = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(inputAzi, 0f, inputAlt) * moveSpeed;
        rigidBodyComponent.MovePosition(rigidBodyComponent.position + movementVector * Time.deltaTime);
    }
}
