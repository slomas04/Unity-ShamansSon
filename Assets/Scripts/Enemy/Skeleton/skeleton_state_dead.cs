using UnityEngine;
using System;

// The skeleton_state_dead class is responsible for handling the dead state of the skeleton enemy
// The skeleton cannot exit this state
public class skeleton_state_dead : EnemyState
{
    private EnemyStateController sc;
    [SerializeField] private static AudioClip SkeletonDie; 

    public skeleton_state_dead(EnemyStateController stateController){
        sc = stateController;
        if (SkeletonDie == null) SkeletonDie = Resources.Load<AudioClip>("Audio/Enemy/SkeletonDie");
    }

    public void OnEnterState(){
        sc.setAnim("SkeletonDead");
        sc.dropItems();
        sc.playSound(SkeletonDie);
    }

    public void OnShot(){

    }

    public void OnUpdate(){
    }
}
