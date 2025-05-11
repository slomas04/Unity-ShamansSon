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
        if (SkeletonStep == null) SkeletonStep = Resources.Load<AudioClip>("Audio/Enemy/SkeletonStep");
        System.Random rnd = new System.Random();
        walkDuration = (float)(rnd.NextDouble() * 1f + 1); // Walk for 1–2 seconds
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
        sc.setIsWalking(true);
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
            if (sc.canSeePlayer())
            {
                sc.setState(new skeleton_state_ready(sc)); 
            }
            else
            {
                sc.setState(new skeleton_state_idle(sc)); 
            }
        }
    }
}