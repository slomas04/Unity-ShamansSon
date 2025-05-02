using Unity.VisualScripting;
using UnityEngine;

public class snake_state_idle : SnakeState
{
    private SnakeStateController sc;

    public snake_state_idle(SnakeStateController stateController){
        sc = stateController;
    }

    public void OnEnterState(){
        sc.setSprite("snake_idle");
    }

    public void OnShot(){

    }

    public void OnUpdate(){
        if (sc.distToPlayer() < SnakeStateController.triggerDist){
            sc.setState(new snake_state_active(sc));
            return;
        }
    }
}
