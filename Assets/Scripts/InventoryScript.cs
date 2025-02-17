using UnityEngine;
using System.Collections.Generic;

// This is purely to silence a bug
// https://stackoverflow.com/questions/64749385/predefined-type-system-runtime-compilerservices-isexternalinit-is-not-defined
namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

[CreateAssetMenu(fileName = "InventoryScript", menuName = "Scriptable Objects/InventoryScript")]
public class InventoryScript : ScriptableObject
{
    public record ItemComb(GenericItem Item, int Quantity);
    public List<ItemComb> items = new();
}
