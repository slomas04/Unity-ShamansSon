using UnityEngine;

public class PhysicsItemBehaviour : MonoBehaviour
{

    private Rigidbody2D rigidBodyComponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // True Advertisement
    private void OnTriggerEnter2D()
    {
        this.rigidBodyComponent.linearVelocityY = (-this.rigidBodyComponent.linearVelocityY) * 0.25f;
    }
}
