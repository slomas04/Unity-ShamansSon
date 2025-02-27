using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{
    [SerializeField] public int index;
    [SerializeField] public GenericItem containedItem;
    [SerializeField] public Image imageRenderer;

    private InventoryController inventoryController;

    private static Sprite DEFAULT_SPRITE;
    
    protected static int nextIndex = 0;

    private void Awake()
    {
        // Set default statics, only once on load
        if (DEFAULT_SPRITE == null)
        {
            DEFAULT_SPRITE = Resources.Load<Sprite>("Sprites/UI/Item_Frame");
        }

        inventoryController = FindAnyObjectByType<InventoryController>();

        index = nextIndex;
        nextIndex++;
        inventoryController.setICSAtIndex(index, this);
    }

    void Start()
    {
        imageRenderer = gameObject.GetComponent<Image>();
        imageRenderer.sprite = DEFAULT_SPRITE;
    }

    void Update()
    {

    }

    private void OnMouseDown()
    {
        
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

}
