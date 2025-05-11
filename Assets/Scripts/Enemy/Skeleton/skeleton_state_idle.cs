using UnityEngine;

public class skeleton_state_idle : EnemyState
{
    private SkeletonStateController sc;
    private float timeEnter;
    private static float idleDuration = 0.4f;

    public skeleton_state_idle(SkeletonStateController stateController)
    {
        sc = stateController;
        timeEnter = Time.time; // Store the time when the state starts
    }

    public void OnEnterState()
    {
        sc.setIsWalking(false);
        sc.setAnim("SkeletonIdle");
        sc.GetComponent<Rigidbody>().linearVelocity = Vector3.zero; // Stop that skeleton!
    }

    public void OnShot()
    {
        PlayerScoreManager.Instance.handleShotHit();
        sc.setState(new skeleton_state_dead(sc));
    }

    public void OnUpdate()
    {
        if (Time.time - timeEnter > idleDuration)
        {
            sc.setState(new skeleton_state_walk(sc));
        }
    }
}
