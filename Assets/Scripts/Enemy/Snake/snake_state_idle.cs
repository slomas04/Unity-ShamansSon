using Unity.VisualScripting;
using UnityEngine;

public class snake_state_idle : EnemyState
{
    private SnakeStateController sc;

    public snake_state_idle(SnakeStateController stateController){
        sc = stateController;
    }

    public void OnEnterState(){
        sc.setAnim("SnakeIdle");
    }

    public void OnShot(){

    }

    public void OnUpdate(){
        if (sc.distToPlayer() < SkeletonStateController.triggerDist){
            sc.setState(new snake_state_active(sc));
            return;
        }
    }
}
