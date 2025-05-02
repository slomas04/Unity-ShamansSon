using Unity.VisualScripting;
using UnityEngine;

public class snake_state_dead : SnakeState
{
    private SnakeStateController sc;

    public snake_state_dead(SnakeStateController stateController){
        sc = stateController;
    }

    public void OnEnterState(){
        sc.setSprite("snake_dead");
    }

    public void OnShot(){

    }

    public void OnUpdate(){

    }
}
