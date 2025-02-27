using UnityEngine;
using UnityEngine.UI;

public class DraggedItemBehaviour : MonoBehaviour
{

    public GenericItem currentItem;
    public Image imageRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageRenderer = gameObject.GetComponent<Image>();
        imageRenderer.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousepos = Input.mousePosition;
        gameObject.transform.position = new Vector3(mousepos.x, mousepos.y, gameObject.transform.position.z);
    }
}
