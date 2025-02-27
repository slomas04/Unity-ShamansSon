using UnityEngine;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{

    [SerializeField] private InventorySlotController[] inventory = new InventorySlotController[21];
    [SerializeField] private int indexPointer = 0;
    
    public void addItem(GenericItem item)
    {
        while(inventory[indexPointer].containedItem != null && indexPointer < 21)
        {
            indexPointer++;
        }
        if (indexPointer < 21) return;
        inventory[indexPointer].setHeldItem(item);
        indexPointer++;
    }

    public void removeItem(int index)
    {
        inventory[index].setHeldItem(null);
        indexPointer = index;
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
