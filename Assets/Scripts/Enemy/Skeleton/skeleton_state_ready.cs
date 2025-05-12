using UnityEngine;

public class skeleton_state_ready : EnemyState
{
    private SkeletonStateController sc;
    private float timeEnterState; 
    private static float eyeShotDelay = 0.4f; 
    [SerializeField] private static AudioClip SkeletonClick;

    public skeleton_state_ready(SkeletonStateController stateController)
    {
        sc = stateController;
        if (SkeletonClick == null) SkeletonClick = Resources.Load<AudioClip>("Audio/Enemy/SkeletonClick");
    }

    public void OnEnterState()
    {
        // Stop the walking animation and set the skeleton to ready state
        sc.setIsWalking(false);
        sc.GetComponent<Rigidbody>().linearVelocity = Vector3.zero; // Stop the Skeleton!
        sc.setAnim("SkeletonReady");
        sc.playSound(SkeletonClick);
        timeEnterState = Time.time; 
    }

    public void OnShot()
    {
        PlayerScoreManager.Instance.handleShotHit();
        sc.setState(new skeleton_state_dead(sc));
    }

    public void OnUpdate()
    {
        // Check that the skeleton can exit the state
        if (Time.time - timeEnterState >= eyeShotDelay)
        {
            // Shoot if in sight of the player
            // If the player is not in sight, wander around
            if (sc.canSeePlayer())
            {
                sc.setState(new skeleton_state_shoot(sc)); 
            }
            else
            {
                sc.setState(new skeleton_state_walk(sc)); 
            }
        }
    }
}