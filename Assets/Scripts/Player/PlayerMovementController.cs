using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 5f;
    public float sprintMultiplier = 2f;
    
    private Rigidbody rb; 

    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        handleJump();
        handleAxisMovement();
    }

    void handleAxisMovement()
    {
        Vector2 axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) 
            * moveSpeed // Apply Default move speed
            * (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f); // Multiply by sprint multiplier if shift down

        // Determine forward vector from camera's right vector, avoids using rb forward vector which lacks camera sync
        Vector3 forward = new Vector3(-Camera.main.transform.right.z, 0f, Camera.main.transform.right.x);

        // Combine velocity of all values
        Vector3 nextVelocity = (forward * axis.x + Camera.main.transform.right * axis.y + Vector3.up * rb.linearVelocity.y);
        rb.linearVelocity = nextVelocity;
    }

    void handleJump()
    {
        if (Input.GetKey(KeyCode.Space) && Physics.Raycast(rb.transform.position, Vector3.down, 1 + 0.001f))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }

}
