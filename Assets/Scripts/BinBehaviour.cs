using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BinBehaviour : MonoBehaviour, IPointerDownHandler
{
    private DraggedItemBehaviour draggedItem;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip binSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        draggedItem = DraggedItemBehaviour.Instance;
    }
    public virtual void OnPointerDown(PointerEventData eventData){
        if (draggedItem.getItem() != null){
            audioSource.PlayOneShot(binSound);
            draggedItem.setItem(null);
        }
    }
}
