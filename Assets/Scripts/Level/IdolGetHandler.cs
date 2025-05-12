using UnityEngine;

public class IdolGetHandler : MonoBehaviour
{
    private bool idolGot = false;
    [SerializeField] private GameObject idol;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        idol.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && !idolGot)
            {
                idolGot = true;
                UIVisibilityController.instance.showIdolUI();
                idol.SetActive(false);
            }
        }
    }
}
