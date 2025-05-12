using System;
using UnityEngine;

public class skeleton_state_shoot : EnemyState
{
    private SkeletonStateController sc;
    private float timeEnter;


    private static float frameDuration = 0.2f;

    // The audio clip for the skeleton's shooting sound
    [SerializeField] private static AudioClip SkeletonShoot; 


    public skeleton_state_shoot(SkeletonStateController stateController)
    {
        sc = stateController;
        timeEnter = Time.time;

        // Load the skeleton shooting sound if not already loaded
        if (SkeletonShoot == null) SkeletonShoot = Resources.Load<AudioClip>("Audio/Enemy/SkeletonFire");
    }

    public void OnEnterState()
    {
        sc.GetComponent<Rigidbody>().linearVelocity = Vector3.zero; // Stop the skeleton!
        Vector3 pos = sc.transform.position + new Vector3(0, 1f, 0);

        // Get the player's position
        Vector3 predictedPosition = sc.predictPlayerPosition();

        // Add a random offset to the predicted position
        Vector3 direction = predictedPosition - pos;
        direction += sc.getRandomOffset();

        // create the projectile and set its direction
        GameObject proj = GameObject.Instantiate(sc.EnemyProjectile, pos, Quaternion.identity);
        proj.transform.forward = direction.normalized;
        Rigidbody rb = proj.GetComponent<Rigidbody>();

        //launch the projectile
        rb.AddForce(direction.normalized * sc.projectileSpeed, ForceMode.Impulse);
        sc.setAnim("SkeletonShoot");
        sc.playSound(SkeletonShoot);
    }

    public void OnShot()
    {
        PlayerScoreManager.Instance.handleShotHit();
        sc.setState(new skeleton_state_dead(sc));
    }

    public void OnUpdate()
    {
        if (Time.time - timeEnter > frameDuration)
        {
            sc.setState(new skeleton_state_walk(sc)); // Transition back to walking
        }
    }
}
