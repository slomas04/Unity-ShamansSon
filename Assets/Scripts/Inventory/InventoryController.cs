using UnityEngine;
using System.Collections.Generic;

public class InventoryScript : MonoBehaviour
{

    public Dictionary<string, ItemCountPair> inventoryDict = new Dictionary<string, ItemCountPair>();
    
    public void addItem(GenericItem item)
    {
        if (inventoryDict.ContainsKey(item.itemName))
        {
            inventoryDict[item.itemName].count++;
        } else
        {
            inventoryDict.Add(item.itemName, new ItemCountPair(item, 1));
        }
    }

    public void removeItem(GenericItem item)
    {
        if (inventoryDict.ContainsKey(item.itemName))
        {
            if (inventoryDict[item.itemName].count > 1)
            {
                inventoryDict[item.itemName].count--;
            } else
            {
                inventoryDict.Remove(item.itemName);
            }
        }
    }
}

public class ItemCountPair
{
    public GenericItem item;
    public int count;

    public ItemCountPair(GenericItem item, int count)
    {
        this.item = item;
        this.count = count;
    }
}
