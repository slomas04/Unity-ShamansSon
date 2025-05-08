using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Animations;
using UnityEngine;

public abstract class EnemyStateController : MonoBehaviour
{
    private EnemyState currentState;
    private Sprite[] sprites;
    private Animator anim;
    private Transform playerTransform;
    private BillboardBehaviour billboard;
    public GameObject EnemyProjectile {get; private set;}
    protected EnemyState initialState;

    [SerializeField] public static float triggerDist = 16f;
    [SerializeField] public static float sightAngle = Mathf.Cos(30 * Mathf.Deg2Rad);

    protected virtual void Awake()
    {
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

}

public interface EnemyState{
    public void OnEnterState();

    public void OnUpdate();

    public void OnShot();

}
