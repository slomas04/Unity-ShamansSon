using UnityEngine;
using UnityEngine.UI;

public class HealthCylinderController : MonoBehaviour
{
    public static HealthCylinderController Instance { get; private set; }

    [SerializeField] private float spinForce = 1.5f;

    private Sprite[] sprites;
    private Image cylinderImage;
    private Rigidbody2D rb;

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        Instance = this;
        sprites = Resources.LoadAll<Sprite>("Sprites/UI/Health_Count");
        cylinderImage = gameObject.GetComponent<Image>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealth(int health)
    {
        print(sprites.Length);
        cylinderImage.sprite = sprites[health];
    }

    public void spin(float force)
    {
        rb.angularVelocity = rb.angularVelocity += (spinForce * force);
    }
}
