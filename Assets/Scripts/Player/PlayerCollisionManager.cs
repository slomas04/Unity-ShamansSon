using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField] private string item_pickup_tag = "Item_Pickup";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == item_pickup_tag){
            GenericItem item = other.GetComponent<PhysicsItemBehaviour>().getContainedItem();
            // Only destroy item if it is added to inventory
            if (InventoryController.Instance.addItem(item)) Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("EnemyBullet")){
            print("Player hit!");
            PlayerHealthManager.Instance.Hit();
            Destroy(collision.gameObject);
        }
    }

}
