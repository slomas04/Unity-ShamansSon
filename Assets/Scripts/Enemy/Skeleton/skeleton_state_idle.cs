using Unity.VisualScripting;
using UnityEngine;
using System;

public class skeleton_state_idle : EnemyState
{
    private EnemyStateController sc;
    private DateTime timeEnter;
    private TimeSpan frameTime;
    private static double idleDuration = 0.4f;

    public skeleton_state_idle(EnemyStateController stateController){
        sc = stateController;
        timeEnter = DateTime.Now;
        frameTime = TimeSpan.FromSeconds(idleDuration);
    }

    public void OnEnterState(){
        sc.setAnim("SkeletonIdle");
    }

    public void OnShot(){

    }

    public void OnUpdate(){
        if(DateTime.Now - timeEnter > frameTime){
            sc.setState(new skeleton_state_walk(sc));
        }
    }
}
