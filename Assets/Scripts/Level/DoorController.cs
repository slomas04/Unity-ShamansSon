using System;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private static float MIN_HEIGHT = 0f;
    [SerializeField] private static float MAX_HEIGHT = 3f;
    [SerializeField] private static float MOVESPEED = 2f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip doorSound;
    private bool goingDown = true;
    private bool doorPressed = false;


    public void castRotation(){
        Vector3 castPos = transform.position + new Vector3(0f, 1f, 0f);

        RaycastHit hitForward;
        Physics.Raycast(castPos, transform.TransformDirection(Vector3.forward), out hitForward);
        RaycastHit hitRight;
        Physics.Raycast(castPos, transform.TransformDirection(Vector3.right), out hitRight);

        if (hitForward.distance < hitRight.distance){
            transform.Rotate(0,90,0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int distMultiplier = goingDown ? -1 : 1;
        float newPos = transform.position.y + (distMultiplier * Time.deltaTime * MOVESPEED);
        if (newPos < MIN_HEIGHT) newPos = MIN_HEIGHT;
        if (newPos > MAX_HEIGHT) newPos = MAX_HEIGHT;

        transform.position = new Vector3(transform.position.x, newPos, transform.position.z);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            goingDown = false;
            if(!doorPressed){
                audioSource.PlayOneShot(doorSound);
                doorPressed = true;
                Invoke("lowerDoor", 5);
            } 
        }

        // Push the player away from the door to prevent the player going out of map
        if (goingDown && transform.position.y > MIN_HEIGHT)
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            Vector3 pushDirection = (other.transform.position - transform.position).normalized;
            pushDirection.y = 0f; 

            playerRb.AddForce(pushDirection * 5f, ForceMode.Impulse);

            Vector3 clampedPosition = playerRb.transform.position;
            clampedPosition.y = Mathf.Max(clampedPosition.y, 0.5f); 
            playerRb.transform.position = clampedPosition;
        }
    }

    private void lowerDoor(){
        goingDown = true;
        doorPressed = false;
        audioSource.PlayOneShot(doorSound);
    }
}
