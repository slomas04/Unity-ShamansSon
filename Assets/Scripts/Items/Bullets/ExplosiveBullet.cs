using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : Bullet
{
    public ExplosiveBullet()
    {
        this.attributes = new Dictionary<string, double>();
        this.itemName = "Explosive Bullet";
        this.description = "Creates an explosion on impact.";
        this.icon = Resources.Load<Sprite>("Sprites/Items/bullet_explosive");
        this.type = ITEM_TYPE.BULLET;
    }
}