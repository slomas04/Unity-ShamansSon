using UnityEngine;

public class UIVisibilityController : MonoBehaviour
{
    public static UIVisibilityController instance { get; private set; }

    [SerializeField] private GameObject gunObject;
    [SerializeField] private GameObject reticile;
    [SerializeField] private GameObject hintMenu;
    [SerializeField] private GameObject IdolUI;

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
        hintMenu.SetActive(false);
        IdolUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        gunObject.SetActive(visible);
        reticile.SetActive(visible);
        hintMenu.SetActive(Input.GetKey(KeyCode.H));
    }

    public void showIdolUI()
    {
        IdolUI.SetActive(true);
        Invoke("hideIdolUI", 5f);
    }

    public void hideIdolUI()
    {
        IdolUI.SetActive(false);
    }

}
