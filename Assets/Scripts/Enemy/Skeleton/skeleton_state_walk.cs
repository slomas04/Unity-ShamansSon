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
        walkDuration = rnd.Next(5,20)/10;
        sc = stateController;
        timeEnter = DateTime.Now;
        frameTime = TimeSpan.FromSeconds(walkDuration);
    }

    public void OnEnterState(){
        sc.setAnim("SkeletonWalkAnimation");
    }

    public void OnShot(){

    }

    public void OnUpdate(){
        if(DateTime.Now - timeEnter > frameTime){
            //next state
            sc.setState(new skeleton_state_walk(sc));
        }
    }
}
