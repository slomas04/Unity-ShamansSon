using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] public int index;
    [SerializeField] public GenericItem containedItem;
    [SerializeField] protected Image imageRenderer;

    protected InventoryController inventoryController;
    protected static Sprite DEFAULT_SPRITE;
    protected static DraggedItemBehaviour ITEM_DRAG;

    private static int nextIndex = 0;

    private void Awake()
    {
        // Set default statics, only once on load
        if (DEFAULT_SPRITE == null) DEFAULT_SPRITE = Resources.Load<Sprite>("Sprites/UI/Item_Frame");

        index = nextIndex;
        nextIndex++;
        containedItem = null;
    }

    private void Start()
    {
        if (ITEM_DRAG == null) ITEM_DRAG = FindAnyObjectByType<DraggedItemBehaviour>();
        inventoryController = FindAnyObjectByType<InventoryController>();
        inventoryController.setICSAtIndex(index, this);
        imageRenderer = gameObject.GetComponent<Image>();
        imageRenderer.sprite = DEFAULT_SPRITE;
    }


    protected virtual void Update()
    {
        imageRenderer.sprite = (containedItem == null) ? DEFAULT_SPRITE : containedItem.icon;
    }


    public void setHeldItem(GenericItem item)
    {
        containedItem = item;
        if (item == null)
        {
            imageRenderer.sprite = DEFAULT_SPRITE;
        } else
        {
            imageRenderer.sprite = item.icon;
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        GenericItem pastCont = (containedItem == null) ? null : (GenericItem) containedItem.Clone();
        containedItem = ITEM_DRAG.getItem();
        ITEM_DRAG.setItem(pastCont);
    }



}
