using UnityEngine;

[CreateAssetMenu(fileName = "GenericItem", menuName = "Scriptable Objects/GenericItem")]
public class GenericItem : ScriptableObject
{
    public string itemName = "Generic Item";
    public string description = "Allan please add details";
    public Sprite icon;
    public string[] attributes = { "a: 4", "b, 5" };
}
