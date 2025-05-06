using Unity.VisualScripting;
using UnityEngine;

public class snake_state_dead : EnemyState
{
    private EnemyStateController sc;

    public snake_state_dead(EnemyStateController stateController){
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
