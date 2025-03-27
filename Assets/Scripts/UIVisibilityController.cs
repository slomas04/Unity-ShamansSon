using UnityEngine;

public class UIVisibilityController : MonoBehaviour
{
    public static UIVisibilityController instance { get; private set; }

    [SerializeField] private GameObject gunObject;
    [SerializeField] private GameObject reticile;

    public bool visible { get; set; }

    private void Awake()
    {
        if (instance) Destroy(gameObject);
        instance = this;
        visible = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gunObject.SetActive(visible);
        reticile.SetActive(visible);
    }

}
