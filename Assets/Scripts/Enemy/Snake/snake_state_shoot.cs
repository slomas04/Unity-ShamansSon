using System;
using UnityEngine;

public class snake_state_shoot : EnemyState
{
    private SnakeStateController sc;
    private float timeEnter;
    private static double frameDuration = 0.4f;
    private static float projSpeed = 80f;
    [SerializeField] private static AudioClip SnakeShoot;

    public snake_state_shoot(SnakeStateController stateController){
        sc = stateController;
        if (SnakeShoot == null) SnakeShoot = Resources.Load<AudioClip>("Audio/Enemy/SnakeFire");
        timeEnter = Time.time;
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
        sc.playSound(SnakeShoot);
        rb.AddForce(direction.normalized * projSpeed, ForceMode.Impulse);
    }

    public void OnEnterState(){
        sc.setAnim("SnakeShoot");
    }

    public void OnShot(){
        PlayerScoreManager.Instance.handleShotHit();
        sc.setState(new snake_state_dead(sc));
    }

    public void OnUpdate(){
        if(Time.time - timeEnter > frameDuration){
            sc.setState(new snake_state_ready(sc));
        }
    }
}
