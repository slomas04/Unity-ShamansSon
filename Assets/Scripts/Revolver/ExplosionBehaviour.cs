using UnityEngine;
using UnityEngine.UIElements;

public class ExplosionBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource audioController;
    [SerializeField] private AnimationClip explosionAnimation;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private float explosionDuration = 0.3f; 
    [SerializeField] private float explosionForce = 500f; 
    [SerializeField] private float explosionRadius = 5f; 

    void Start()
    {
        // Ensure the explosion only exists for as long as the sound plays
        audioController.PlayOneShot(explosionSound);
        animator.SetFloat("PlaybackSpeed", explosionAnimation.length / explosionSound.length);
        animator.SetTrigger("Explode");
        Destroy(gameObject, explosionSound.length);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                rb.AddForce(direction * explosionForce, ForceMode.Impulse);
            }
            if (collider.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<EnemyStateController>().applyExplosion();
            }
        }
    }
}
