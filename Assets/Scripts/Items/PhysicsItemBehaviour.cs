using Unity.VisualScripting;
using UnityEngine;

public class PhysicsItemBehaviour : MonoBehaviour
{

    [SerializeField] private GenericItem containedItem;
    [SerializeField] private SphereCollider colliderSmall;

    private SpriteRenderer spriteR;

    void Awake()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SphereCollider colliderSmall = gameObject.GetComponent<SphereCollider>();
        colliderSmall.radius = 0.3f;
        colliderSmall.tag = "Item_Pickup";
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, 75 * Time.deltaTime, 0));
    }

    public void setContainedItem(GenericItem i){
        containedItem = i;
        spriteR.sprite = i.icon;
    }


    public GenericItem getContainedItem(){
        return containedItem;
    }

}
