using System.Collections.Generic;
using UnityEngine;

public class Bullet : GenericItem
{
    public Bullet()
    {
        this.attributes = new Dictionary<string, double>();
        this.itemName = "Bullet";
        this.description = "Pointy, made to go fast, sort of boring actually.";
        this.icon = Resources.Load<Sprite>("Sprites/Items/bullet_default");
        attributes.Add("Penetration", 1);

    }
}