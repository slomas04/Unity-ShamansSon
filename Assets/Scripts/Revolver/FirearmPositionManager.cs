using UnityEngine;

public class FirearmPositionManager : MonoBehaviour
{
    public static FirearmPositionManager instance { get; private set; }

    private RectTransform goTransform;

    [SerializeField] private float height;
    [SerializeField] private float width;
    [SerializeField] private bool isReloading;

    private void Awake()
    {
        if (instance) Destroy(gameObject);
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goTransform = gameObject.GetComponent<RectTransform>();
        setGunSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setGunSize()
    {
        RectTransform parent = gameObject.GetComponentInParent<RectTransform>();
        float halfHeight = parent.rect.height / 2.0f;
        width = halfHeight;
        height = halfHeight;
        goTransform.sizeDelta = new Vector2(width, height);
    }

    public void setFirePosition()
    {
        Vector2 anchor = new Vector2(1, 0);
        goTransform.anchorMin = anchor;
        goTransform.anchorMax = anchor;
        goTransform.anchoredPosition = new Vector3(0, 0, 0);
    }

    public void setFired()
    {
        goTransform.sizeDelta = new Vector2(width, height * 2);
    }

    public void unsetFired()
    {
        goTransform.sizeDelta = new Vector2(width, height);
    }

    public void setReloadPosition()
    {
        Vector2 anchor = new Vector2(0, 0);
        goTransform.anchorMin = anchor;
        goTransform.anchorMax = anchor;
        goTransform.anchoredPosition = new Vector3(width, 0, 0);
    }
}
