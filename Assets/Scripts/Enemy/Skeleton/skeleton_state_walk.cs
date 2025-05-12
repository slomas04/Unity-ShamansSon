using UnityEngine;

public class skeleton_state_walk : EnemyState
{
    // Change directions every 1 second
    private static float directionChangeInterval = 1f; 

    // Move at a speed of 3 units per second
    private static float moveSpeed = 3f;

    // Take a step every 0.3 seconds
    private static float stepInterval = 0.3f;
    private SkeletonStateController sc;
    private float timeEnter;

    // The duration for the skeleton to walk
    private float walkDuration;
    private float timeSinceLastChange;
    private float timeSinceLastStep;     

    // The direction the skeleton is wandering in
    private Vector3 wanderDirection;
    private Rigidbody rb;

    // The skeleton's step sound
    [SerializeField] private static AudioClip SkeletonStep; 


    public skeleton_state_walk(SkeletonStateController stateController)
    {
        // Load the skeleton step sound if not already loaded
        if (SkeletonStep == null) SkeletonStep = Resources.Load<AudioClip>("Audio/Enemy/SkeletonStep");
        System.Random rnd = new System.Random();

        // Set the walk duration to a random value between 1 and 2 seconds
        walkDuration = (float)(rnd.NextDouble() * 1f + 1); 
        sc = stateController;

        // Set the initial time for entering the state
        timeEnter = Time.time; 
        rb = sc.GetComponent<Rigidbody>();

        // Set the initial direction for wandering
        wanderDirection = Random.insideUnitSphere;
        wanderDirection.y = 0;
        wanderDirection.Normalize();
    }

    public void OnEnterState()
    {
        // Set the skeleton's animation to walking
        sc.setAnim("SkeletonWalk");
        sc.setIsWalking(true);
        timeSinceLastStep = 0f; 
    }

    public void OnShot()
    {   
        // Add to the player's score when the skeleton is shot
        PlayerScoreManager.Instance.handleShotHit();
        sc.setState(new skeleton_state_dead(sc));
    }

    public void OnUpdate()
    {
        timeSinceLastChange += Time.deltaTime;
        timeSinceLastStep += Time.deltaTime;

        // Change the direction of the skeleton every directionChangeInterval seconds
        if (timeSinceLastChange >= directionChangeInterval)
        {
            wanderDirection = Random.insideUnitSphere;
            wanderDirection.y = 0; 
            wanderDirection.Normalize();
            timeSinceLastChange = 0f;
        }

        // Play the skeleton's step sound every stepInterval seconds
        if (timeSinceLastStep >= stepInterval)
        {
            sc.playSound(SkeletonStep);
            timeSinceLastStep = 0f;
        }

        rb.linearVelocity = wanderDirection * moveSpeed;

        // Check if the skeleton has been in this state for longer than walkDuration
        if (Time.time - timeEnter > walkDuration)
        {   
            // If the skeleton can see the player, change to the ready state
            // Otherwise, change to the idle state
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