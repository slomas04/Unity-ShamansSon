using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class TypeConstrainedInventorySlotController : InventorySlotController
{
    [SerializeField] private GenericItem.ITEM_TYPE typeConstraint;
    [SerializeField] private Sprite overlaySprite;

    private Image overlayImage;

    private void Awake()
    {
        // Set default statics, only once on load
        if (DEFAULT_SPRITE == null) DEFAULT_SPRITE = Resources.Load<Sprite>("Sprites/UI/Item_Frame");
        containedItem = null;
    }

    private void Start()
    {
        if (ITEM_DRAG == null) ITEM_DRAG = FindAnyObjectByType<DraggedItemBehaviour>();
        imageRenderer = gameObject.GetComponent<Image>();
        imageRenderer.sprite = DEFAULT_SPRITE;
        overlayImage = GetComponentsInChildren<Image>()[1]; //Jank
    }

    protected override void Update()
    {
        base.Update();
        if(overlayImage.sprite != overlaySprite)
        {
            overlayImage.sprite = overlaySprite;
        }
        if(base.containedItem != null)
        {
            overlayImage.enabled = false;
        } else
        {
            overlayImage.enabled = true;
        }
    }

    public void setType(GenericItem.ITEM_TYPE type)
    {
        if(type == GenericItem.ITEM_TYPE.NONE)
        {
            throw new System.ArgumentException("Item Type cannot be null!");
        }
        typeConstraint = type;
        overlaySprite = Resources.Load<Sprite>("Sprites/UI/slot_" + Enum.GetName(typeof(GenericItem.ITEM_TYPE), type).ToLower());
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if(ITEM_DRAG.getItem() == null || ITEM_DRAG.getItem().type == typeConstraint)
        {
            base.OnPointerDown(eventData);
        }
    }
}
