using UnityEngine;
using System.Collections.Generic;

public class ItemCasing : GenericItem
{
    public enum CASING_SIZE
    {
        SMALL,
        MEDIUM,
        LARGE
    }

    public ItemCasing(CASING_SIZE size)
    {
        attributes = new Dictionary<string, double>();
        this.type = ITEM_TYPE.CASING;
        switch (size)
        {
            case CASING_SIZE.SMALL:
                this.itemName = "Small Casing";
                this.description = "A small bullet casing.";
                this.icon = Resources.Load<Sprite>("Sprites/Items/casing_small");
                attributes.Add("capacity", 3);
                break;
            case CASING_SIZE.MEDIUM:
                this.itemName = "Medium Casing";
                this.description = "A medium bullet casing.";
                this.icon = Resources.Load<Sprite>("Sprites/Items/casing_med");
                attributes.Add("capacity", 4);

                break;
            case CASING_SIZE.LARGE:
                this.itemName = "Large Casing";
                this.description = "A large bullet casing.";
                this.icon = Resources.Load<Sprite>("Sprites/Items/casing_large");
                attributes.Add("capacity", 5);

                break;
        }
    }
}
