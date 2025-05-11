using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public abstract class EnemyStateController : MonoBehaviour
{
    private EnemyState currentState;
    private Sprite[] sprites;
    private Animator anim;
    private Transform playerTransform;
    private BillboardBehaviour billboard;
    public GameObject EnemyProjectile {get; private set;}
    protected EnemyState initialState;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] public static float triggerDist = 24f;
    [SerializeField] public static float sightAngle = Mathf.Cos(30 * Mathf.Deg2Rad);
    [SerializeField] private AudioSource audioController;

    protected virtual void Awake()
    {
        itemPrefab = Resources.Load<GameObject>("Prefabs/ItemPickup");
        EnemyProjectile = Resources.Load<GameObject>("Prefabs/EnemyProjectile");
        anim = GetComponent<Animator>();
        billboard = GetComponent<BillboardBehaviour>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        currentState = initialState;
        currentState.OnEnterState();
    }

    void Update()
    {
        currentState.OnUpdate();
    }

    public void setState(EnemyState s){
        currentState = s;
        s.OnEnterState();
    }

    public float distToPlayer(){
        Vector3 pos = transform.position;
        pos.y = 0;
        Vector3 playerPos = playerTransform.position;
        playerPos.y = 0;
        return Vector3.Distance(pos, playerPos);
    }

    public Transform getPlayer(){
        return playerTransform;
    }

    public BillboardBehaviour getBillboard(){
        return billboard;
    }

    public void setAnim(String name){
        anim.SetTrigger(name);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet")){
            currentState.OnShot();
        }
    }

    public bool canSeePlayer(){

        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0f;

        if (Vector3.Dot(transform.forward, directionToPlayer.normalized) < sightAngle) return false;

        Vector3 eyePosition = transform.position + Vector3.up * 1.5f;
        RaycastHit hit;
        if (Physics.Raycast(eyePosition, directionToPlayer.normalized, out hit, directionToPlayer.magnitude)){
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    public void dropItems()
    {
        List<GenericItem> itemsToDrop = new List<GenericItem>{
            new ItemCasing(ItemCasing.CASING_SIZE.SMALL),
            new Primer(),
            new Bullet(),
            new Gunpowder(),
            getRandomGenericItem()
        };

        foreach (var item in itemsToDrop)
        {
            GameObject itemObject = Instantiate(itemPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);

            PhysicsItemBehaviour itemBehaviour = itemObject.GetComponent<PhysicsItemBehaviour>();
            if (itemBehaviour != null)
            {
                itemBehaviour.setContainedItem(item);
            }

            // Add some force to the item to make it move about
            Rigidbody rb = itemObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 1.5f), Random.Range(-1f, 1f)).normalized;
                float randomForce = Random.Range(1f, 3f);
                rb.linearVelocity = randomDirection * randomForce;
            }
        }
    }

    private GenericItem getRandomGenericItem()
    {
        GenericItem.ITEM_TYPE[] itemTypes = { GenericItem.ITEM_TYPE.CASING, GenericItem.ITEM_TYPE.PRIMER, GenericItem.ITEM_TYPE.BULLET, GenericItem.ITEM_TYPE.POWDER };
        GenericItem.ITEM_TYPE randomType = itemTypes[Random.Range(0, itemTypes.Length)];

        switch (randomType)
        {
            case GenericItem.ITEM_TYPE.CASING:
                return new ItemCasing((ItemCasing.CASING_SIZE)Random.Range(0, 3)); 
            case GenericItem.ITEM_TYPE.PRIMER:
                return new Primer();
            case GenericItem.ITEM_TYPE.BULLET:
                return new Bullet();
            case GenericItem.ITEM_TYPE.POWDER:
                return new Gunpowder();
            default:
                return null;
        }
    }

    public void playSound(AudioClip clip)
    {
            audioController.PlayOneShot(clip);
    }
}

public interface EnemyState{
    public void OnEnterState();

    public void OnUpdate();

    public void OnShot();

}
