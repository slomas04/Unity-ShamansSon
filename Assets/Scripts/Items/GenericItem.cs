using UnityEngine;
using System.Collections.Generic;

// This supresses a unity error associated with defining records
namespace System.Runtime.CompilerServices
{
    internal class IsExternalInit { }
}

[CreateAssetMenu(fileName = "GenericItem", menuName = "Scriptable Objects/GenericItem")]
[System.Serializable]
public class GenericItem : ScriptableObject
{
    public record attributeRecord(string attribute, float value);

    public string itemName = "Generic Item";
    public string description = "Allan please add details";
    public Sprite icon;
    public List<attributeRecord> attributes = new List<attributeRecord> { new attributeRecord("Test Value", 5f)};
}
