using UnityEngine;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{

    [SerializeField] private Dictionary<int, InventorySlotController> inventory = new Dictionary<int, InventorySlotController>();
    [SerializeField] public const int INVENTORY_SIZE = 21;

    public void Awake()
    {
        for (int i = 0; i < INVENTORY_SIZE; i++)
        {
            inventory.Add(i, null);
        }
    }

    public bool addItem(GenericItem item)
    {
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
        return isSet;
    }

    public void removeItem(int index)
    {
        inventory[index].setHeldItem(null);
    }

    public void setItemAtIndex(int index, GenericItem item)
    {
        inventory[index].setHeldItem(item);
    }

    public void setICSAtIndex(int index, InventorySlotController ics)
    {
        inventory[index] = ics;
    }
}
