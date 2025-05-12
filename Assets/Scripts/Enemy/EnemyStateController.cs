using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

// This is the base class for handling the AI of both enemies
public abstract class EnemyStateController : MonoBehaviour
{
    // The current EnemyState of the entity
    private EnemyState currentState;

    // Reference to the Animator component
    protected Animator anim;

    // Reference to the Player's Transform and Rigidbody
    private Transform playerTransform;
    private Rigidbody playerRigidbody;

    // Reference to the BillboardBehaviour component
    private BillboardBehaviour billboard;

    // Reference to the EnemyProjectile prefab
    [SerializeField] public GameObject EnemyProjectile;

    // Reference to the item prefab, for dropping items
    [SerializeField] private GameObject itemPrefab;

    // Reference to the initial state of the enemy
    protected EnemyState initialState;

    // The minimum distance for the enemy to trigger an action
    [SerializeField] public float triggerDist = 24f;

    // The max angle for the enemy to see the player
    [SerializeField] public float sightAngle = Mathf.Cos(30 * Mathf.Deg2Rad);

    // The speed of the enemy's projectiles
    [SerializeField] public float projectileSpeed = 75f;

    // How bad (in units) the enemy's aim is
    [SerializeField] private float aimOffset = 0.5f;

    // The audio controller for playing sounds
    [SerializeField] private AudioSource audioController;

    protected virtual void Awake()
    {
        // Get components for the entity
        anim = GetComponent<Animator>();
        billboard = GetComponent<BillboardBehaviour>();
    }

    void Start()
    {
        // Get the necessary components of the player
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        playerRigidbody = playerTransform.gameObject.GetComponent<Rigidbody>();

        // Set the initial state of the enemy
        currentState = initialState;
        currentState.OnEnterState();
    }

    void Update()
    {
        // Update the current state of the enemy
        currentState.OnUpdate();
    }

    public void setState(EnemyState s){
        // Trigger state entering for the enemy
        currentState = s;
        s.OnEnterState();
    }

    // Get the distance to the player
    public float distToPlayer(){
        Vector3 pos = transform.position;
        pos.y = 0;
        Vector3 playerPos = playerTransform.position;
        playerPos.y = 0;
        return Vector3.Distance(pos, playerPos);
    }

    // Get the player's transform
    public Transform getPlayer(){
        return playerTransform;
    }

    // Get the entity's billboard
    public BillboardBehaviour getBillboard(){
        return billboard;
    }

    public void setAnim(String name){
        anim.SetTrigger(name);
    }
    void OnCollisionEnter(Collision collision)
    {   
        // Call the OnShot method of the current state if the enemy is hit by a bullet
        if (collision.gameObject.CompareTag("PlayerBullet")){
            currentState.OnShot();
        }
    }

    // The explosion uses triggers, so we can call this instead.
    public void applyExplosion(){
        currentState.OnShot();
    }

    // Check if the entity can see the player
    public bool canSeePlayer(){

        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0f;

        // Get the dot product of the forward vector and the direction to the player
        if (Vector3.Dot(transform.forward, directionToPlayer.normalized) < sightAngle) return false;

        // Cast a ray from the enemy to the player
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

    // Try to predict the player's position
    public Vector3 predictPlayerPosition()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float distance = directionToPlayer.magnitude;

        // Add a cap to the prediction distance to prevent the enemies becoming MLG pros
        float timeToTarget = Mathf.Min(distance / projectileSpeed, 1.5f);

        Vector3 predictedPosition = playerTransform.position + playerRigidbody.linearVelocity * timeToTarget;

        return predictedPosition;
    }

    // Get a random offset for the aim
    public Vector3 getRandomOffset()
    {
        return new Vector3(Random.Range(-aimOffset, aimOffset), Random.Range(-aimOffset, aimOffset), Random.Range(-aimOffset, aimOffset));
    }

    // Drop items when the enemy is killed
    public void dropItems()
    {
        // The enemy drops a casing, primer, bullet, and gunpowder and a random item
        List<GenericItem> itemsToDrop = new List<GenericItem>{
            new ItemCasing(ItemCasing.CASING_SIZE.SMALL),
            new Primer(),
            new Bullet(),
            new Gunpowder(),
            getRandomGenericItem()
        };

        foreach (var item in itemsToDrop)
        {
            // Create a new item object
            GameObject itemObject = Instantiate(itemPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);

            // Set the contents of the item object
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

    // Get a random item to drop
    private GenericItem getRandomGenericItem()
    {
        GenericItem.ITEM_TYPE[] itemTypes = { GenericItem.ITEM_TYPE.CASING, GenericItem.ITEM_TYPE.PRIMER, GenericItem.ITEM_TYPE.BULLET, GenericItem.ITEM_TYPE.POWDER };
        GenericItem.ITEM_TYPE randomType = itemTypes[Random.Range(0, itemTypes.Length)];

        switch (randomType)
        {
            case GenericItem.ITEM_TYPE.CASING:
                return new ItemCasing((ItemCasing.CASING_SIZE)Random.Range(0, 3)); 
            case GenericItem.ITEM_TYPE.PRIMER:
                return (Random.Range(0,2) == 1) ? new Primer() : new HealingPrimer();
            case GenericItem.ITEM_TYPE.BULLET:
                return (Random.Range(0,2) == 1) ? new Bullet() : new ExplosiveBullet();
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

    public bool isDead()
    {
        return currentState is skeleton_state_dead || currentState is snake_state_dead;
    }
}

// EnemyState subclass for the enemy's state
public interface EnemyState{
    public void OnEnterState();

    public void OnUpdate();

    public void OnShot();

}
