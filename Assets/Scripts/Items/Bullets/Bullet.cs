using System.Collections.Generic;
using UnityEngine;

public class Bullet : GenericItem
{
    public Bullet()
    {
        this.attributes = new List<attributeRecord>();
        this.itemName = "Bullet";
        this.description = "Pointy, made to go fast, sort of boring actually.";
        this.icon = Resources.Load<Sprite>("Sprites/Items/bullet_default");
        attributes.Add(new attributeRecord("Penetration", 1));

    }
}