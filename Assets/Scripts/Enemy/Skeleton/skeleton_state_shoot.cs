using System;
using UnityEngine;

public class skeleton_state_shoot : EnemyState
{
    private EnemyStateController sc;
    private float timeEnter;
    private static double frameDuration = 0.2f;
    private static float projSpeed = 80f;

    public skeleton_state_shoot(EnemyStateController stateController){
        sc = stateController;
        timeEnter = Time.time;
    }

    public void OnEnterState(){
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
        sc.setAnim("SkeletonShoot");
        rb.AddForce(direction.normalized * projSpeed, ForceMode.Impulse);
    }

    public void OnShot(){
        sc.setState(new skeleton_state_dead(sc));
    }

    public void OnUpdate(){
        if(Time.time - timeEnter > frameDuration){
            sc.setState(new skeleton_state_walk(sc));
        }
    }
}
