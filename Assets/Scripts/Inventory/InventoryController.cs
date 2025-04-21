using UnityEngine;
using System.Collections.Generic;


/* Inventory Controller
 * This is a component of the top-level inventory object
 * It handles the storage, adding and removal of Items
 */
public class InventoryController : MonoBehaviour
{
    // We store a list of all InventorySlotControllers, where each controller assigns itself a slot in their Start() method.
    [SerializeField] private Dictionary<int, InventorySlotController> inventory = new Dictionary<int, InventorySlotController>();
    [SerializeField] public const int INVENTORY_SIZE = 21;

    public void Awake()
    {
        // We need to set all inventory slots to Null to prevent an exception
        for (int i = 0; i < INVENTORY_SIZE; i++)
        {
            inventory.Add(i, null);
        }
    }

    // This method returns a bool so that interacting objects may run processes if the inventory is full.
    public bool addItem(GenericItem item)
    {
        // Looks for next available inventory slot then inserts the item there
        bool isSet = false;
        foreach(KeyValuePair<int,InventorySlotController> entry in inventory)
        {
            if (entry.Value.containedItem == null && !isSet)
            {
                isSet = true;
                entry.Value.setHeldItem(item);
                return true;
            }
        }
        // If an item is not found, we return false.
        return isSet;
    }

    public void removeItem(int index)
    {
        inventory[index].setHeldItem(null);
    }

    public int? getItemIndex(GenericItem item){
        foreach(KeyValuePair<int,InventorySlotController> entry in inventory)
        {
            if (entry.Value.containedItem != null)
            {
                if (entry.Value.containedItem.itemName == item.itemName) return entry.Key;
            }
        }

        return null;
    }

    public void setItemAtIndex(int index, GenericItem item)
    {
        inventory[index].setHeldItem(item);
    }

    // InventorySlotControllers inside the inventory will assign themselves an index using this method.
    public void setICSAtIndex(int index, InventorySlotController ics)
    {
        inventory[index] = ics;
    }
}
