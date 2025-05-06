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
    public GameObject EnemyProjectile {get; private set;}
    
    protected EnemyState initialState;

    [SerializeField] public static float triggerDist = 16f;

    protected virtual void Awake()
    {
        EnemyProjectile = Resources.Load<GameObject>("Prefabs/EnemyProjectile");
        anim = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        currentState = initialState;
        currentState.OnEnterState();
    }

    // Update is called once per frame
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

    public void setAnim(String name){
        anim.SetTrigger(name);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet")){
            currentState.OnShot();
        }
    }
}

public interface EnemyState{
    public void OnEnterState();

    public void OnUpdate();

    public void OnShot();

}
