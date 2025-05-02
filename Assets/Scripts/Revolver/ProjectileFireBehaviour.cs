using System.Numerics;
using UnityEngine;
using System;

public class ProjectileFireBehaviour : MonoBehaviour
{

    [SerializeField] private cBullet containedBullet = null;
    [SerializeField] private double liveTime = 20; // Lifespan of bullet
    private TimeSpan lifespan;
    private DateTime creation;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lifespan = TimeSpan.FromSeconds(liveTime);
        creation = DateTime.UtcNow;
        rb = GetComponent<Rigidbody>();
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
                GameObject.Destroy(gameObject);
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
}
