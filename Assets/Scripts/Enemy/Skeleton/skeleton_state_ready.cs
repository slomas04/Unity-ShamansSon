using UnityEngine;

public class skeleton_state_ready : EnemyState
{
    private EnemyStateController sc;
    private float lastEyeShot;
    private static float eyeShotSeconds = 0.4f;

    public skeleton_state_ready(EnemyStateController stateController){
        sc = stateController;
        lastEyeShot = float.MaxValue;
    }

    public void OnEnterState(){
        sc.setAnim("SkeletonReady");
    }

    public void OnShot(){
        sc.setState(new skeleton_state_dead(sc));
    }

    public void OnUpdate(){

        bool rayOnPlayer = false;
        RaycastHit hit;
        Ray ray = new Ray(sc.transform.position, sc.transform.forward);
        if(Physics.Raycast(ray, out hit, EnemyStateController.triggerDist)){
            if(hit.collider.gameObject.CompareTag("Player")){
                rayOnPlayer = true;
            }
        } 

        if (rayOnPlayer) {
            if (lastEyeShot == float.MaxValue) {
                lastEyeShot = Time.time;
            } else {
                if (Time.time - lastEyeShot > eyeShotSeconds) {
                    lastEyeShot = float.MaxValue;
                    sc.setState(new skeleton_state_shoot(sc));
                }
            }
        } else {
            lastEyeShot = float.MaxValue;
            sc.setState(new skeleton_state_walk(sc));
            return;
        }
        
        

    }
}
