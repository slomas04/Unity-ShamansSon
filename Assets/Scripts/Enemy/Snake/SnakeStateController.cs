using System;
using UnityEngine;

public class SnakeStateController : MonoBehaviour
{
    private SnakeState currentState;
    private Sprite[] snake_sprites;
    private SpriteRenderer sr;
    private Transform playerTransform;
    public GameObject EnemyProjectile {get; private set;}

    [SerializeField] public static float triggerDist = 16f;

    void Awake()
    {
        snake_sprites = Resources.LoadAll<Sprite>("Sprites/Enemies/Snake/");
        EnemyProjectile = Resources.Load<GameObject>("Prefabs/EnemyProjectile");
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        currentState = new snake_state_idle(this);
        currentState.OnEnterState();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    public void setState(SnakeState s){
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
        foreach (Sprite s in snake_sprites){
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

public interface SnakeState{
    public void OnEnterState();

    public void OnUpdate();

    public void OnShot();

}
