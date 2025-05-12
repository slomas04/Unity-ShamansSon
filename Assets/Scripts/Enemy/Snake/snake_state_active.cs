using System;
using UnityEngine;

public class snake_state_active : EnemyState
{
    private SnakeStateController sc;
    private float lastEyeShot;
    private static float eyeShotSeconds = 0.5f;
    [SerializeField] private static AudioClip SnakeHiss;

    public snake_state_active(SnakeStateController stateController){
        // Load the audio clip for the snake hiss sound
        if (SnakeHiss == null) SnakeHiss = Resources.Load<AudioClip>("Audio/Enemy/SnakeHiss");
        sc = stateController;
        lastEyeShot = float.MaxValue;

        // Hiss sound is played when the snake enters the active state
        sc.playSound(SnakeHiss);
    }

    public void OnEnterState(){
        sc.setAnim("SnakeActive");
    }

    public void OnShot(){
        PlayerScoreManager.Instance.handleShotHit();
        sc.setState(new snake_state_dead(sc));
    }

    public void OnUpdate(){
        // If the player is too far away, go idle
        if (sc.distToPlayer() > sc.triggerDist){
            sc.setState(new snake_state_idle(sc));
            return;
        }

        bool playerInCone = sc.canSeePlayer();

        // If the player is in sight of the snake, go to ready state
        if (playerInCone){
            if (lastEyeShot == float.MaxValue){
                lastEyeShot = Time.time;
            }

            if (Time.time - lastEyeShot > eyeShotSeconds){
                lastEyeShot = float.MaxValue;  
                sc.setState(new snake_state_ready(sc));
                return;
            }
        } else {
            lastEyeShot = float.MaxValue;
        }
        
    }
}
