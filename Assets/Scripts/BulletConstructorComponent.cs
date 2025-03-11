using UnityEngine;
using UnityEngine.UI;

public class BulletConstructorComponent : MonoBehaviour
{
    [SerializeField] private GameObject casingSlot;
    [SerializeField] private TypeConstrainedInventorySlotController slotController;
    [SerializeField] private TypeConstrainedInventorySlotController[] bulletSlots;
    [SerializeField] private GameObject prefab;

    private int currentSize;
    private static Sprite CASING_START;
    private static Sprite CASING_MAIN;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slotController = casingSlot.GetComponent<TypeConstrainedInventorySlotController>();
        if (CASING_START == null)
        {
            CASING_START = Resources.Load<Sprite>("Sprites/UI/casing_start");
            CASING_MAIN = Resources.Load<Sprite>("Sprites/UI/casing_body");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (slotController.containedItem != null)
        {
            int casingSize = (int) slotController.containedItem.attributes["capacity"];
            if (casingSize != currentSize)
            {
                currentSize = casingSize;
                bulletSlots = new TypeConstrainedInventorySlotController[casingSize];
                for (int i = 0; i < casingSize; i++)
                {
                    GameObject instChild = Instantiate(prefab, gameObject.transform, true);

                    if (i == 0)
                    {
                        instChild.GetComponent<Image>().sprite = CASING_START;
                        bulletSlots[i] = instChild.GetComponentInChildren<TypeConstrainedInventorySlotController>();
                        bulletSlots[i].setType(GenericItem.ITEM_TYPE.PRIMER);


                    } else if (i == (casingSize - 1))
                    {
                        instChild.GetComponent<Image>().enabled = false;
                        bulletSlots[i] = instChild.GetComponentInChildren<TypeConstrainedInventorySlotController>();
                        bulletSlots[i].setType(GenericItem.ITEM_TYPE.BULLET);
                    }
                    else
                    {
                        instChild.GetComponent<Image>().sprite = CASING_MAIN;
                        bulletSlots[i] = instChild.GetComponentInChildren<TypeConstrainedInventorySlotController>();
                        bulletSlots[i].setType(GenericItem.ITEM_TYPE.POWDER);
                    }

                }
            }
        } else
        {
            // Destroy all children if no casing in item slot
            while (transform.childCount > 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
            currentSize = 0;
        }
    }
}
