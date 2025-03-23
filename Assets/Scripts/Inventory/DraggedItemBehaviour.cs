using UnityEngine;
using UnityEngine.UI;

/* Dragged Item Behaviour
 * This behaviour handles the behaviour of the DraggedItem object.
 * When the inventory is shown, it follows the mouse.
 * It stores one GenericItem, which it displays over the mouse.
 * InventorySlotControllers contain references to this, and can take and place items in it.
 */
public class DraggedItemBehaviour : MonoBehaviour
{
    // The item being held
    [SerializeField] private GenericItem CurrentItem;
    // The object's Image component
    public Image imageRenderer;
    // Store a reference to the canvas group to modify Object's alpha
    public CanvasGroup cGroup;

    void Start()
    {
        CurrentItem = null;
        imageRenderer = gameObject.GetComponent<Image>();
        cGroup = gameObject.GetComponent<CanvasGroup>();
        imageRenderer.sprite = null;
    }

    void Update()
    {
        imageRenderer.sprite = (CurrentItem == null) ? null : CurrentItem.icon;

        // Hide object by setting its alpha to 0 if it's holding nothing
        cGroup.alpha = (CurrentItem == null) ? 0f : 0.5f;

        // Update object position
        Vector2 mousepos = Input.mousePosition;
        gameObject.transform.position = new Vector3(mousepos.x, mousepos.y, gameObject.transform.position.z);
    }

    /*
     * Basic getters and setters for the Item
     */
    public GenericItem getItem() { return CurrentItem; }
    public void setItem(GenericItem item) { CurrentItem = item; }
}
