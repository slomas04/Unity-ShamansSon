using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreateBulletBehaviour : MonoBehaviour
{
    private Button buttonObj;
    [SerializeField] private BulletConstructorComponent constructor;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip createBulletSound;
    private BulletSackController sack;
    private TypeConstrainedInventorySlotController casingSelector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonObj = gameObject.GetComponent<Button>();
        buttonObj.onClick.AddListener(handleMakeBullet);
        constructor = BulletConstructorComponent.Instance;
        sack = BulletSackController.Instance;
        casingSelector = GameObject.Find("CasingSelector")
            .GetComponent<TypeConstrainedInventorySlotController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void handleMakeBullet(){
        TypeConstrainedInventorySlotController[] componentArr = constructor.getBulletSlots();
        if (componentArr.Length < 1) return;
        GenericItem[] bulletComponents = new GenericItem[componentArr.Length];
        for (int i = 0; i < componentArr.Length; i++){
            // Don't make bullet if slots are not filled
            if (componentArr[i].containedItem == null){
                return;
            }
            bulletComponents[i] = componentArr[i].containedItem;
        }

        cBullet b = new cBullet(bulletComponents);
        bool newCasingResult = casingSelector.handleBulletCreate();
        foreach (TypeConstrainedInventorySlotController s in componentArr){
            if (newCasingResult){
                s.handleBulletCreate();
            } else {
                s.containedItem = null;
            }
        }
        audioSource.PlayOneShot(createBulletSound);
        sack.addItem(b);
    }
}
