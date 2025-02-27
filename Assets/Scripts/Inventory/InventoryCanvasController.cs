using System;
using UnityEngine;

public class InventoryCanvasController : MonoBehaviour
{
    public bool isVisible = false;
    public GameObject inventoryGO;

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
    }
}
