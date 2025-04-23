using System.Numerics;
using UnityEngine;

public class ProjectileFireBehaviour : MonoBehaviour
{

    [SerializeField] private cBullet containedBullet = null;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (containedBullet != null){
            UnityEngine.Vector3 newVec = new UnityEngine.Vector3(containedBullet.ProjSpeed * Time.deltaTime, containedBullet.ProjSpeed * Time.deltaTime, containedBullet.ProjSpeed * Time.deltaTime);
            transform.position = transform.position + newVec;
        }
    }

    public void setBullet(cBullet b){
        containedBullet = b;
    }
}
