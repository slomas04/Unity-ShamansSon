using UnityEngine;
using System.Collections.Generic;

// This supresses a unity error associated with defining records
namespace System.Runtime.CompilerServices
{
    internal class IsExternalInit { }
}

[System.Serializable]
public abstract class GenericItem : System.ICloneable
{
    public record attributeRecord(string attribute, float value);
    public enum ITEM_TYPE { NONE, PRIMER, CASING, BULLET, POWDER};

    public string itemName = "Generic Item";
    public string description = "Allan please add details";
    public Sprite icon;
    public List<attributeRecord> attributes = new List<attributeRecord> { new attributeRecord("Test Value", 5f)};
    public ITEM_TYPE type = ITEM_TYPE.NONE;

    protected GenericItem() { }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
