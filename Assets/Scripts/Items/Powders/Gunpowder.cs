using System.Collections.Generic;
using UnityEngine;

public class Gunpowder : GenericItem
{
    public Gunpowder()
    {
        this.attributes = new Dictionary<string, double>();
        this.itemName = "Gunpowder";
        this.description = "The basic stuff, goes bang.";
        this.icon = Resources.Load<Sprite>("Sprites/Items/powder_default");
        this.type = ITEM_TYPE.POWDER;
        attributes.Add("Force Multiplier", 1);

    }
}
