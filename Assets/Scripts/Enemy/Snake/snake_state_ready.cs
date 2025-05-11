using UnityEngine;

public class snake_state_ready : EnemyState
{
    private SnakeStateController sc;
    private float lastEyeShot;
    private static double eyeShotSeconds = 0.5f;
    [SerializeField] private static AudioClip SnakeClick;

    public snake_state_ready(SnakeStateController stateController){
        sc = stateController;
        lastEyeShot = float.MaxValue;

        if (SnakeClick == null) SnakeClick = Resources.Load<AudioClip>("Audio/Enemy/SnakeClick");
    }

    public void OnEnterState(){
        sc.setAnim("SnakeReady");
        sc.playSound(SnakeClick);
    }

    public void OnShot(){
        PlayerScoreManager.Instance.handleShotHit();
        sc.setState(new snake_state_dead(sc));
    }

    public void OnUpdate(){
        if (sc.distToPlayer() > sc.triggerDist){
            sc.setState(new snake_state_idle(sc));
            return;
        }

        bool playerInCone = sc.canSeePlayer();

        if (playerInCone){
            if (lastEyeShot == float.MaxValue){
                lastEyeShot = Time.time;
            }

            if (Time.time - lastEyeShot > eyeShotSeconds){
                lastEyeShot = Time.time;  
                sc.setState(new snake_state_shoot(sc));
            }
        } else {
            lastEyeShot = Time.time;
            return;
        }
        
        

    }
}
