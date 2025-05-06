using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class snake_state_active : EnemyState
{
    private EnemyStateController sc;
    private DateTime lastEyeShot;
    private TimeSpan eyeShotTime;

    private static double eyeShotSeconds = 0.5f;

    public snake_state_active(EnemyStateController stateController){
        sc = stateController;
        lastEyeShot = DateTime.MaxValue;
        eyeShotTime = TimeSpan.FromSeconds(eyeShotSeconds);
    }

    public void OnEnterState(){
        sc.setSprite("snake_active");
    }

    public void OnShot(){
        sc.setState(new snake_state_dead(sc));
    }

    public void OnUpdate(){
        if (sc.distToPlayer() > EnemyStateController.triggerDist){
            sc.setState(new snake_state_idle(sc));
            return;
        }

        bool rayOnPlayer = false;
        RaycastHit hit;
        Ray ray = new Ray(sc.transform.position, sc.transform.forward);
        if(Physics.Raycast(ray, out hit, EnemyStateController.triggerDist)){
            if(hit.collider.gameObject.CompareTag("Player")){
                rayOnPlayer = true;
            }
        } 

        if (rayOnPlayer){
            if (lastEyeShot == DateTime.MaxValue){
                lastEyeShot = DateTime.Now;
            }

            if (DateTime.Now - lastEyeShot > eyeShotTime){
                lastEyeShot = DateTime.MaxValue;  
                sc.setState(new snake_state_ready(sc));
                return;
            }
        } else {
            lastEyeShot = DateTime.MaxValue;
        }
        
        

    }
}
