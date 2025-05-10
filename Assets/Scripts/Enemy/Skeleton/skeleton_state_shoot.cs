using System;
using UnityEngine;

public class skeleton_state_shoot : EnemyState
{
    private SkeletonStateController sc;
    private float timeEnter;
    private static double frameDuration = 0.2f;
    private static float projSpeed = 80f;
    [SerializeField] private static AudioClip SkeletonShoot; 


    public skeleton_state_shoot(SkeletonStateController stateController){
        sc = stateController;
        timeEnter = Time.time;
        if (SkeletonShoot == null) SkeletonShoot = Resources.Load<AudioClip>("Audio/Enemy/SkeletonFire");
    }

    public void OnEnterState(){
        Vector3 pos = sc.transform.position + new Vector3(0, 1f, 0);

        // HANDLE BULLET FIRING LOGIC
        Ray ray = new Ray(pos, sc.transform.forward);
        RaycastHit hitPoint;
        Vector3 targetPosition;

        if(Physics.Raycast(ray, out hitPoint)){
            targetPosition = hitPoint.point;
        } else {
            targetPosition = ray.GetPoint(60);
        }

        Vector3 direction = targetPosition - pos;

        GameObject proj = GameObject.Instantiate(sc.EnemyProjectile, pos, Quaternion.identity);
        proj.transform.forward = direction.normalized;
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        sc.setAnim("SkeletonShoot");
        sc.playSound(SkeletonShoot);
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
