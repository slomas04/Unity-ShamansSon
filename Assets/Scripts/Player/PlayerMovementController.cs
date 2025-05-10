using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 5f;
    public float sprintMultiplier = 2f;
    public double headBobMultiplier = 0.006;

    [SerializeField] private double distance;

    private Rigidbody rb; 
    private Camera mainCamera;

    [SerializeField] private AudioSource audioSource; 
    [SerializeField] private AudioClip stepSound;
    [SerializeField] private AudioClip jumpSound;


    private bool isAtTop = false; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        distance = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerHealthManager.Instance.IsDead) return;
        handleJump();
        handleAxisMovement();
    }

    void Update()
    {
        if (PlayerHealthManager.Instance.IsDead) return;

        // Bob head, but only if on the ground
        if (transform.position.y < 1.1)
        {
            if (rb.linearVelocity != Vector3.zero)
            {
                distance += (headBobMultiplier * 2.0) * rb.linearVelocity.magnitude;
            }
            bobHead();
        }
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
            audioSource.PlayOneShot(jumpSound);
        }
    }

    void bobHead()
    {
        double cameraHeight = 0.5 + (0.05 * Math.Cos(distance));
        mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x, (float)cameraHeight, mainCamera.transform.localPosition.z);

        double bobValue = Math.Cos(distance);
        if (bobValue > 0.99 && !isAtTop) 
        {
            PlayStepSound(1.0f);
            isAtTop = true;
        }
        else if (bobValue < -0.99 && isAtTop) 
        {
            PlayStepSound(0.8f); 
            isAtTop = false;
        }
    }

    void PlayStepSound(float pitch)
    {
        float prevpitch = audioSource.pitch;
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(stepSound);
        audioSource.pitch = prevpitch;
    }
}