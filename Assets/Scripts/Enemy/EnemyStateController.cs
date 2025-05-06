using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class EnemyStateController : MonoBehaviour
{
    private EnemyState currentState;
    private Sprite[] sprites;
    private SpriteRenderer sr;
    private Transform playerTransform;
    public GameObject EnemyProjectile {get; private set;}
    
    protected string spriteSheetPath;
    protected EnemyState initialState;

    [SerializeField] public static float triggerDist = 16f;

    protected virtual void Awake()
    {
        sprites = Resources.LoadAll<Sprite>(spriteSheetPath);
        EnemyProjectile = Resources.Load<GameObject>("Prefabs/EnemyProjectile");
        sr = GetComponent<SpriteRenderer>();
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

    public void setSprite(String name){
        foreach (Sprite s in sprites){
            if(s.name == name) sr.sprite = s;
        }
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
