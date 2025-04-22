using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

/* Type Constrained Inventory Slot Controller
 * Wordy name, does the same thing as InventorySlotController but is intended
 * for use in the bullet constructor and is limited to a certain type of item.
 */
public class TypeConstrainedInventorySlotController : InventorySlotController
{
    // The type of Item that this slot is limited to
    [SerializeField] private GenericItem.ITEM_TYPE typeConstraint;

    // The Sprite of the Required item that should be overlayed over the inventory slot
    [SerializeField] private Sprite overlaySprite;

    private Image overlayImage;

    private void Awake()
    {
        // Set default statics, only once on load
        if (DEFAULT_SPRITE == null) DEFAULT_SPRITE = Resources.Load<Sprite>("Sprites/UI/Item_Slots/Item_Frame");
        containedItem = null;
    }

    private void Start()
    {
        if (ITEM_DRAG == null) ITEM_DRAG = FindAnyObjectByType<DraggedItemBehaviour>();
        imageRenderer = gameObject.GetComponent<Image>();
        imageRenderer.sprite = DEFAULT_SPRITE;
        overlayImage = GetComponentsInChildren<Image>()[1]; //Jank, for some reason this seemed to get the Image component of this object if done normally
    }

    protected override void Update()
    {
        base.Update();
        if(overlayImage.sprite != overlaySprite)
        {
            overlayImage.sprite = overlaySprite;
        }

        // Hide the overlay image if there is an item in the slot
        if(base.containedItem != null)
        {
            overlayImage.enabled = false;
        } else
        {
            overlayImage.enabled = true;
        }
    }

    // Method to set the type constraint, if the object is Instantiated from a prefab.
    public void setType(GenericItem.ITEM_TYPE type)
    {
        if(type == GenericItem.ITEM_TYPE.NONE)
        {
            throw new System.ArgumentException("Item Type cannot be null!");
        }
        typeConstraint = type;

        // Slightly janky method to load a sprite for the overlay by the string version of the Item type.
        overlaySprite = Resources.Load<Sprite>("Sprites/UI/Item_Slots/slot_" + Enum.GetName(typeof(GenericItem.ITEM_TYPE), type).ToLower());
    }

    // Do not call the parent class's version of the method unless the constraint is satisfied.
    public override void OnPointerDown(PointerEventData eventData)
    {
        if(ITEM_DRAG.getItem() == null || ITEM_DRAG.getItem().type == typeConstraint)
        {
            base.OnPointerDown(eventData);
        }
    }

    // Return true if a new item was got
    public bool handleBulletCreate(){
        int? nextItemIndex = inventoryController.getItemIndex(containedItem);
        if (nextItemIndex == null){
            containedItem = null;
            return false;
        }
        inventoryController.removeItem(nextItemIndex.Value);
        return true;
    }

    public GenericItem.ITEM_TYPE getTypeConstraint(){
        return typeConstraint;
    }
}
