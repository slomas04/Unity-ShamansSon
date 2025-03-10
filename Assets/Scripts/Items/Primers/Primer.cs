using System.Collections.Generic;
using UnityEngine;

public class Primer : GenericItem
{
    public Primer()
    {
        this.attributes = new List<attributeRecord>();
        this.itemName = "Primer";
        this.description = "Like those things that you put in cap guns.";
        this.icon = Resources.Load<Sprite>("Sprites/Items/primer_default");
        attributes.Add(new attributeRecord("delay", 10));

    }
}