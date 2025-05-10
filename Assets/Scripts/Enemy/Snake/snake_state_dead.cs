using Unity.VisualScripting;
using UnityEngine;

public class snake_state_dead : EnemyState
{
    private SnakeStateController sc;

    public snake_state_dead(SnakeStateController stateController){
        sc = stateController;
    }

    public void OnEnterState(){
        sc.setAnim("SnakeDead");
        sc.playSound(sc.SnakeDie);
        sc.dropItems();
    }

    public void OnShot(){

    }

    public void OnUpdate(){

    }
}
