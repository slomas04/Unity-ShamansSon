using Unity.VisualScripting;
using UnityEngine;

public class snake_state_dead : EnemyState
{
    private SnakeStateController sc;
    [SerializeField] private static AudioClip SnakeDie;

    public snake_state_dead(SnakeStateController stateController){
        sc = stateController;

        if (SnakeDie == null) SnakeDie = Resources.Load<AudioClip>("Audio/Enemy/SnakeDie");
    }

    public void OnEnterState(){
        sc.setAnim("SnakeDead");
        sc.playSound(SnakeDie);
        sc.dropItems();
    }

    public void OnShot(){

    }

    public void OnUpdate(){

    }
}
