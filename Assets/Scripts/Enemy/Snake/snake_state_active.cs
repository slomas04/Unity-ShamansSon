using System;
using UnityEngine;

public class snake_state_active : EnemyState
{
    private SnakeStateController sc;
    private float lastEyeShot;
    private static float eyeShotSeconds = 0.5f;

    public snake_state_active(SnakeStateController stateController){
        sc = stateController;
        lastEyeShot = float.MaxValue;
        sc.playSound(sc.SnakeHiss);
    }

    public void OnEnterState(){
        sc.setAnim("SnakeActive");
    }

    public void OnShot(){
        sc.setState(new snake_state_dead(sc));
    }

    public void OnUpdate(){
        if (sc.distToPlayer() > SkeletonStateController.triggerDist){
            sc.setState(new snake_state_idle(sc));
            return;
        }

        bool playerInCone = sc.canSeePlayer();

        if (playerInCone){
            if (lastEyeShot == float.MaxValue){
                lastEyeShot = Time.time;
            }

            if (Time.time - lastEyeShot > eyeShotSeconds){
                lastEyeShot = float.MaxValue;  
                sc.setState(new snake_state_ready(sc));
                return;
            }
        } else {
            lastEyeShot = float.MaxValue;
        }
        
    }
}
