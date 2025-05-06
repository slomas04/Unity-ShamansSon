using Unity.VisualScripting;
using UnityEngine;
using System;

public class skeleton_state_walk : EnemyState
{
    private EnemyStateController sc;
    private DateTime timeEnter;
    private TimeSpan frameTime;
    private double walkDuration;

    public skeleton_state_walk(EnemyStateController stateController){
        System.Random rnd = new System.Random();
        walkDuration = rnd.Next(2,7)/10;
        sc = stateController;
        timeEnter = DateTime.Now;
        frameTime = TimeSpan.FromSeconds(idleDuration);
    }

    public void OnEnterState(){
        sc.setSprite("Skeleton_Idle");
    }

    public void OnShot(){

    }

    public void OnUpdate(){
        if(DateTime.Now - timeEnter > frameTime){
            sc.setState(new skeleton_state_walk(sc));
        }
    }
}
