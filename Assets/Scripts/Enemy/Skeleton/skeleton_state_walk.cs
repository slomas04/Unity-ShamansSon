using UnityEngine;

public class skeleton_state_walk : EnemyState
{
    private static float directionChangeInterval = 1f; 
    private static float moveSpeed = 3f;
    private static float stepInterval = 0.3f;

    private SkeletonStateController sc;
    private float timeEnter;
    private float walkDuration;
    private float timeSinceLastChange;
    private float timeSinceLastStep;     
    private Vector3 wanderDirection;
    private Rigidbody rb;
    [SerializeField] private static AudioClip SkeletonStep; 


    public skeleton_state_walk(SkeletonStateController stateController)
    {
        System.Random rnd = new System.Random();
        if (SkeletonStep == null) SkeletonStep = Resources.Load<AudioClip>("Audio/Enemy/SkeletonStep");
        walkDuration = (float)(rnd.NextDouble() * 2f + 1); 
        sc = stateController;
        timeEnter = Time.time; 
        rb = sc.GetComponent<Rigidbody>();

        wanderDirection = Random.insideUnitSphere;
        wanderDirection.y = 0;
        wanderDirection.Normalize();
    }

    public void OnEnterState()
    {
        sc.setAnim("SkeletonWalk");
        timeSinceLastStep = 0f; 
    }

    public void OnShot()
    {   
        PlayerScoreManager.Instance.handleShotHit();
        sc.setState(new skeleton_state_dead(sc));
    }

    public void OnUpdate()
    {
        timeSinceLastChange += Time.deltaTime;
        timeSinceLastStep += Time.deltaTime;

        if (timeSinceLastChange >= directionChangeInterval)
        {
            wanderDirection = Random.insideUnitSphere;
            wanderDirection.y = 0; 
            wanderDirection.Normalize();
            timeSinceLastChange = 0f;
        }

        if (timeSinceLastStep >= stepInterval)
        {
            sc.playSound(SkeletonStep);
            timeSinceLastStep = 0f;
        }

        rb.linearVelocity = wanderDirection * moveSpeed; 

        if (Time.time - timeEnter > walkDuration)
        {
            sc.setState(sc.canSeePlayer() ? new skeleton_state_ready(sc) : new skeleton_state_idle(sc));
        }
    }
}