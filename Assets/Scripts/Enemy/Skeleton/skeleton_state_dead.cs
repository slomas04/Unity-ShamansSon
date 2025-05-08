using UnityEngine;
using System;

public class skeleton_state_dead : EnemyState
{
    private EnemyStateController sc;

    public skeleton_state_dead(EnemyStateController stateController){
        sc = stateController;
    }

    public void OnEnterState(){
        sc.setAnim("SkeletonDead");
    }

    public void OnShot(){

    }

    public void OnUpdate(){
    }
}
