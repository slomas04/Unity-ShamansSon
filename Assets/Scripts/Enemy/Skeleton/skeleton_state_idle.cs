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
        sc.setAnim("SkeletonIdle");
    }

    public void OnShot()
    {
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
