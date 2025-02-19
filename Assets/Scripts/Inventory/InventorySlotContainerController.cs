using UnityEngine;
using System.Collections.Generic;

public class InventorySlotContainerController : MonoBehaviour
{
    public const int INVENTORY_SLOT_COUNT = 27;
    public List<GameObject> inventorySlots;
    public GameObject prefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventorySlots = new List<GameObject>();

        // Create Inventory Slots
        for (int i = 0; i < INVENTORY_SLOT_COUNT; i++)
        {
            inventorySlots.Add(Instantiate(prefab, this.transform, true));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
