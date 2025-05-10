using UnityEngine;
using System;

public class skeleton_state_dead : EnemyState
{
    private SkeletonStateController sc;

    public skeleton_state_dead(SkeletonStateController stateController){
        sc = stateController;
    }

    public void OnEnterState(){
        sc.setAnim("SkeletonDead");
        sc.dropItems();
    }

    public void OnShot(){

    }

    public void OnUpdate(){
    }
}
