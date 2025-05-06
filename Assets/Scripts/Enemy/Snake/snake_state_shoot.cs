using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class snake_state_shoot : EnemyState
{
    private EnemyStateController sc;
    private DateTime timeEnter;
    private TimeSpan frameTime;

    private static double frameDuration = 0.4f;
    private static float projSpeed = 80f;

    public snake_state_shoot(EnemyStateController stateController){
        sc = stateController;
        timeEnter = DateTime.Now;
        frameTime = TimeSpan.FromSeconds(frameDuration);

        // HANDLE BULLET FIRING LOGIC
        Ray ray = new Ray(sc.transform.position, sc.transform.forward);
        RaycastHit hitPoint;
        Vector3 targetPosition;

        if(Physics.Raycast(ray, out hitPoint)){
            targetPosition = hitPoint.point;
        } else {
            targetPosition = ray.GetPoint(60);
        }

        Vector3 pos = sc.transform.position + new Vector3(0,1.5f,0);

        Vector3 direction = targetPosition - pos;

        GameObject proj = GameObject.Instantiate(sc.EnemyProjectile, pos, Quaternion.identity);

        proj.transform.forward = direction.normalized;
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        rb.AddForce(direction.normalized * projSpeed, ForceMode.Impulse);
    }

    public void OnEnterState(){
        sc.setSprite("snake_shoot");
    }

    public void OnShot(){
        sc.setState(new snake_state_dead(sc));
    }

    public void OnUpdate(){
        if(DateTime.Now - timeEnter > frameTime){
            sc.setState(new snake_state_ready(sc));
        }
    }
}
