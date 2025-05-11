using System;
using UnityEngine;
using UnityEngine.UIElements;

public class snake_state_shoot : EnemyState
{
    private SnakeStateController sc;
    private float timeEnter;
    private static double frameDuration = 0.4f;
    [SerializeField] private static AudioClip SnakeShoot;

    public snake_state_shoot(SnakeStateController stateController){
        sc = stateController;
        if (SnakeShoot == null) SnakeShoot = Resources.Load<AudioClip>("Audio/Enemy/SnakeFire");
        timeEnter = Time.time;
    }

    public void OnEnterState(){
        Vector3 pos = sc.transform.position + new Vector3(0, 1f, 0);
        Vector3 predictedPosition = sc.predictPlayerPosition();

        Vector3 direction = predictedPosition - pos;
        direction += sc.getRandomOffset();

        GameObject proj = GameObject.Instantiate(sc.EnemyProjectile, pos, Quaternion.identity);
        proj.transform.forward = direction.normalized;
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        rb.AddForce(direction.normalized * sc.projectileSpeed, ForceMode.Impulse);
        sc.setAnim("SnakeShoot");
        sc.playSound(SnakeShoot);

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
