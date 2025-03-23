using UnityEngine;
using System.Collections.Generic;

/* Inventory Slot Container Controller
 * This class simply creates N many inventory slots inside the inventory slot window.
 */
public class InventorySlotContainerController : MonoBehaviour
{
    public List<GameObject> inventorySlots;
    public GameObject prefab;


    void Start()
    {
        inventorySlots = new List<GameObject>();

        // Create Inventory Slots
        for (int i = 0; i < InventoryController.INVENTORY_SIZE; i++)
        {
            inventorySlots.Add(Instantiate(prefab, this.transform, true));
        }
    }

}
