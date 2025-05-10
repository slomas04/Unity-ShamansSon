using UnityEngine;

public class skeleton_state_ready : EnemyState
{
    private SkeletonStateController sc;
    private float lastEyeShot;
    private static float eyeShotSeconds = 0.4f;
    [SerializeField] private static AudioClip SkeletonClick; 

    public skeleton_state_ready(SkeletonStateController stateController){
        sc = stateController;
        lastEyeShot = float.MaxValue;
        if (SkeletonClick == null) SkeletonClick = Resources.Load<AudioClip>("Audio/Enemy/SkeletonClick");
    }

    public void OnEnterState(){
        sc.setAnim("SkeletonReady");
        sc.playSound(SkeletonClick);
    }

    public void OnShot(){
        sc.setState(new skeleton_state_dead(sc));
    }

    public void OnUpdate(){

        bool rayOnPlayer = sc.canSeePlayer();

        if (rayOnPlayer) {
            if (lastEyeShot == float.MaxValue) {
                lastEyeShot = Time.time;
            } else {
                if (Time.time - lastEyeShot > eyeShotSeconds) {
                    lastEyeShot = float.MaxValue;
                    sc.setState(new skeleton_state_shoot(sc));
                }
            }
        } else {
            lastEyeShot = float.MaxValue;
            sc.setState(new skeleton_state_walk(sc));
            return;
        }
        
        

    }
}
