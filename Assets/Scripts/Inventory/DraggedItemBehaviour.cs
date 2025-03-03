using UnityEngine;
using UnityEngine.UI;

public class DraggedItemBehaviour : MonoBehaviour
{

    [SerializeField] private GenericItem currentItem;
    public Image imageRenderer;
    public CanvasGroup cGroup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentItem = null;
        imageRenderer = gameObject.GetComponent<Image>();
        cGroup = gameObject.GetComponent<CanvasGroup>();
        imageRenderer.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        imageRenderer.sprite = (currentItem == null) ? null : currentItem.icon;
        cGroup.alpha = (currentItem == null) ? 0f : 0.5f;
        Vector2 mousepos = Input.mousePosition;
        gameObject.transform.position = new Vector3(mousepos.x, mousepos.y, gameObject.transform.position.z);
    }

    public GenericItem getItem() { return currentItem; }
    public void setItem(GenericItem item) { currentItem = item; }
}
