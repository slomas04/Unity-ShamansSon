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
        if (Time.time - timeEnterState >= eyeShotDelay)
        {
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