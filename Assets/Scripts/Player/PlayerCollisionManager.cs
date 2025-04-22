using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField] private string item_large_tag = "Item_Large";
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
            InventoryController.Instance.addItem(item);
            GameObject.Destroy(other.gameObject);
        }
    }

}
