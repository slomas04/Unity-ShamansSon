using System.Collections.Generic;
using UnityEngine;

public class Gunpowder : GenericItem
{
    public Gunpowder()
    {
        this.attributes = new List<attributeRecord>();
        this.itemName = "Gunpowder";
        this.description = "The basic stuff, goes bang.";
        this.icon = Resources.Load<Sprite>("Sprites/Items/powder_default");
        attributes.Add(new attributeRecord("Force Multiplier", 1));

    }
}
