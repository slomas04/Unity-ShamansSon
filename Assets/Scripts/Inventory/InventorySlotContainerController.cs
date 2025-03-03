using UnityEngine;
using System.Collections.Generic;

public class InventorySlotContainerController : MonoBehaviour
{
    public List<GameObject> inventorySlots;
    public GameObject prefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventorySlots = new List<GameObject>();

        // Create Inventory Slots
        for (int i = 0; i < InventoryController.INVENTORY_SIZE; i++)
        {
            inventorySlots.Add(Instantiate(prefab, this.transform, true));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
