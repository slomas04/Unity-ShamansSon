using System;
using UnityEngine;

/* Inventory Canvas Controller
 * This component is attached to the Inventory canvas itself
 * It mostly just handles toggling the visibility of the inventory
 * This may be merged with another controller in the future, it seems like too much of a stub.
 */
public class InventoryCanvasController : MonoBehaviour
{
    // Store the inventory visibility
    public bool isVisible = false;
    // Store a reference to the Inventory, for adding items
    public GameObject inventoryGO;

    // Only allow dev items to be added once
    private bool hasAddedItems = false;

    void Update()
    {
        // Check for Tab being pressed, this opens the inventory.
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isVisible = !isVisible;
            UIVisibilityController.instance.visible = !isVisible;
            SendMessage("toggleInventoryShow", isVisible);
        }
        inventoryGO.SetActive(isVisible);

        // Add dev items on left bracket down, this may be removed in the future.
        if(Input.GetKeyDown(KeyCode.LeftBracket))
        {
            addDevItems();
        }
    }

    // Add a set of useful items for testing
    void addDevItems()
    {
        if (hasAddedItems) return;
        InventoryController inventoryController = FindAnyObjectByType<InventoryController>();
        for (int i = 0; i < 2; i++)
        {
            inventoryController.addItem(new ItemCasing(ItemCasing.CASING_SIZE.SMALL));
            inventoryController.addItem(new ItemCasing(ItemCasing.CASING_SIZE.MEDIUM));
            inventoryController.addItem(new ItemCasing(ItemCasing.CASING_SIZE.LARGE));
            inventoryController.addItem(new Bullet());
            inventoryController.addItem(new Primer());
            inventoryController.addItem(new Gunpowder());

        }

        BulletSackController sack = BulletSackController.Instance;
        GenericItem[] items = { new Bullet(), new Gunpowder(), new Gunpowder(), new Primer()};
        for (int i = 0; i < 12; i++) sack.addItem(new cBullet(items));
        hasAddedItems = true;
    }
}
