using System;
using UnityEngine;

public class InventoryCanvasController : MonoBehaviour
{
    public bool isVisible = false;
    public GameObject inventoryGO;

    private bool hasAddedItems = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isVisible = !isVisible;
            SendMessage("toggleInventoryShow", isVisible);
        }
        inventoryGO.SetActive(isVisible);
        if(Input.GetKeyDown(KeyCode.LeftBracket))
        {
            addDevItems();
        }
    }

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
        hasAddedItems = true;
    }
}
