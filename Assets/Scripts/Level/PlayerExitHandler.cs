using UnityEngine;

public class PlayerExitHandler : MonoBehaviour
{
    private BoxCollider boxCol;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCol = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E)){
            print("End Level!");
        }
    }
}
