using UnityEngine;

public class HealingPrimer : Primer
{
    public HealingPrimer()
    {
        this.itemName = "Healing Primer";
        this.description = "Heals the player when fired.";
        this.icon = Resources.Load<Sprite>("Sprites/Items/primer_healing");
        this.type = ITEM_TYPE.PRIMER;
    }
}