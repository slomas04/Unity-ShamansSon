using UnityEngine;
using System;

public class ProjectileFireBehaviour : MonoBehaviour
{

    [SerializeField] private cBullet containedBullet = null;
    [SerializeField] private double liveTime = 20; // Lifespan of bullet
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip ricochetSound;

    private TimeSpan lifespan;
    private DateTime creation;
    private Vector3 initialPosition;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lifespan = TimeSpan.FromSeconds(liveTime);
        creation = DateTime.UtcNow;
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (DateTime.UtcNow - creation > lifespan){
            GameObject.Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(gameObject.CompareTag("PlayerBullet")){
            if (collision.gameObject.tag != "Player"){
                switch(containedBullet.HeadType){
                    case cBullet.HEAD_TYPE.HE:
                        handleExplosion(collision.contacts[0].point);
                        Destroy(gameObject);
                        break;
                    default:
                        Destroy(gameObject);
                        break;
                }
            }
        } else {
            if (!collision.gameObject.CompareTag("Enemy")){
                Destroy(gameObject);
            }
        }
        
    }

    public void setBullet(cBullet b){
        containedBullet = b;
    }

    // Create an explosion at the collision point
    private void handleExplosion(Vector3 explosionPosition)
    {
        // Shift the explosion a bit towards the player to avoid clipping
        Vector3 directionToContact = (explosionPosition - initialPosition).normalized;
        Vector3 adjustedPosition = explosionPosition - directionToContact * 2f; 
        
        Instantiate(explosionPrefab, adjustedPosition, Quaternion.identity);
    }

}
