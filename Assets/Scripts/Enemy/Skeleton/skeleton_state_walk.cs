using UnityEngine;

public class skeleton_state_walk : EnemyState
{
    private static float directionChangeInterval = 1f; 
    private static float moveSpeed = 3f;

    private EnemyStateController sc;
    private float timeEnter;
    private float walkDuration;
    private float timeSinceLastChange;
    private Vector3 wanderDirection;
    private Rigidbody rb;

    public skeleton_state_walk(EnemyStateController stateController)
    {
        System.Random rnd = new System.Random();
        walkDuration = (float)(rnd.NextDouble() * 2f + 1); 
        sc = stateController;
        timeEnter = Time.time; 
        rb = sc.GetComponent<Rigidbody>();

        wanderDirection = Random.insideUnitSphere;
        wanderDirection.y = 0;
        wanderDirection.Normalize();
    }

    public void OnEnterState()
    {
        sc.setAnim("SkeletonWalk");
    }

    public void OnShot()
    {
        sc.setState(new skeleton_state_dead(sc));
    }

    public void OnUpdate()
    {

        timeSinceLastChange += Time.deltaTime;

        if (timeSinceLastChange >= directionChangeInterval)
        {
            wanderDirection = Random.insideUnitSphere;
            wanderDirection.y = 0; 
            wanderDirection.Normalize();
            timeSinceLastChange = 0f;
        }

        rb.linearVelocity = wanderDirection * moveSpeed; 

        if (Time.time - timeEnter > walkDuration)
        {
            RaycastHit hit;
            Ray ray = new Ray(sc.transform.position, sc.transform.forward);
            if (Physics.Raycast(ray, out hit, EnemyStateController.triggerDist) && hit.collider.gameObject.CompareTag("Player"))
            {
                sc.setState(new skeleton_state_ready(sc));
            }
            else
            {
                sc.setState(new skeleton_state_idle(sc));
            }
        }
    }
}
