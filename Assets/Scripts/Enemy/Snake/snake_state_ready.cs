using UnityEngine;

public class snake_state_ready : EnemyState
{
    private EnemyStateController sc;
    private float lastEyeShot;
    private static double eyeShotSeconds = 0.5f;

    public snake_state_ready(EnemyStateController stateController){
        sc = stateController;
        lastEyeShot = float.MaxValue;
    }

    public void OnEnterState(){
        sc.setAnim("SnakeReady");
    }

    public void OnShot(){
        sc.setState(new snake_state_dead(sc));
    }

    public void OnUpdate(){
        if (sc.distToPlayer() > EnemyStateController.triggerDist){
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
