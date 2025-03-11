using System.Collections.Generic;
using UnityEngine;

public class Primer : GenericItem
{
    public Primer()
    {
        this.attributes = new Dictionary<string, double>();
        this.itemName = "Primer";
        this.description = "Like those things that you put in cap guns.";
        this.icon = Resources.Load<Sprite>("Sprites/Items/primer_default");
        this.type = ITEM_TYPE.PRIMER;
        attributes.Add("delay", 10);

    }
}