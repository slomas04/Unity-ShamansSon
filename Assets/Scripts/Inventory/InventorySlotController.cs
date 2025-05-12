using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* Inventory Slot Controller
 * This component handles each inventory slot.
 * IPointerDownHandler is implemented for handling mouse clicks on inventory slots.
 */
public class InventorySlotController : MonoBehaviour, IPointerDownHandler
{
    // Serialized fields to display the index, contained item and image renderer for each Slot.
    [SerializeField] public int index;
    [SerializeField] public GenericItem containedItem;
    [SerializeField] protected Image imageRenderer;
    [SerializeField] private AudioClip binSound;
    [SerializeField] private AudioClip insertSound;
    [SerializeField] private AudioClip itemAddSound;

    // Protected statics handle object references that every Slot should keep.
    // They are instanced only once for the sake of efficency.
    protected static InventoryController inventoryController;
    protected static Sprite DEFAULT_SPRITE;
    protected DraggedItemBehaviour dragged_item;

    // Static int used to keep each slot's index unique.
    private static int nextIndex = 0;
    private static AudioSource audioSource;


    private void Awake()
    {
        // Set default statics, only once on load
        if (DEFAULT_SPRITE == null) DEFAULT_SPRITE = Resources.Load<Sprite>("Sprites/UI/Item_Slots/Item_Frame");

        // Assign and increment index
        index = nextIndex;
        nextIndex++;
        containedItem = null;
    }

    private void Start()
    {
        if (audioSource == null) audioSource = GameObject.Find("FirearmAudioSource").GetComponent<AudioSource>();

        // Get Drag and inventory objects if they are null
        dragged_item = DraggedItemBehaviour.Instance;
        inventoryController = InventoryController.Instance;

        // Set the index of the slot in the inventory controller here
        inventoryController.setICSAtIndex(index, this);
        imageRenderer = gameObject.GetComponent<Image>();
        imageRenderer.sprite = DEFAULT_SPRITE;
    }

    // Set slot icon to that of the item's icon, if it exists
    protected virtual void Update()
    {
        imageRenderer.sprite = (containedItem == null) ? DEFAULT_SPRITE : containedItem.icon;
    }

    // Can be called from inventoryController to assign this slot an item when one is picked up.
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

    // Handles mousedown events on this Object.
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftShift)){
            if (BulletConstructorComponent.Instance.handleShiftInsert(containedItem)) containedItem = null;
            audioSource.PlayOneShot(insertSound);
        } else if (Input.GetKey(KeyCode.LeftControl)){
            containedItem = null;
            audioSource.PlayOneShot(binSound);
        } else {
            GenericItem pastCont = (containedItem == null) ? null : (GenericItem) containedItem.Clone();
            containedItem = dragged_item.getItem();
            dragged_item.setItem(pastCont);
            audioSource.PlayOneShot(insertSound);
        }
        
    }

    public void playItemAdd(){
        audioSource.PlayOneShot(itemAddSound);
    }

}
