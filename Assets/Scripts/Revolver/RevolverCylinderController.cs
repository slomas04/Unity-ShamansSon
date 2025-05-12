using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class RevolverCylinderController : MonoBehaviour
{
    public static RevolverCylinderController Instance {get; private set;}
    public enum CHAMBER_STATE { EMPTY, LOADED, FIRED };

    private BulletSackController sack;
    private Image chamberImage;
    private GameObject chamberDecal;
    private TMP_Text textBox;

    private Sprite chamberEmpty;
    private Sprite chamberFull;
    private GameObject projectilePrefab;
    private GameObject player;
    private Camera playerCamera;

    [SerializeField] public const int CYLINDER_SIZE = 6;
    [SerializeField] private cBullet[] cylinderArr;
    [SerializeField] private CHAMBER_STATE[] cylinderStateArr;
    [SerializeField] private int barrelPosition;
    [SerializeField] private int gatePosition;

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        Instance = this;
        chamberEmpty = Resources.Load<Sprite>("Sprites/UI/Chamber/Chamber_empty");
        chamberFull = Resources.Load<Sprite>("Sprites/UI/Chamber/Chamber_full");

        resetChamberState();
    }

    public void resetChamberState(){
        cylinderArr = new cBullet[CYLINDER_SIZE];
        Array.ForEach(cylinderArr, b => b = null);
        cylinderStateArr = new CHAMBER_STATE[CYLINDER_SIZE];
        Array.ForEach(cylinderStateArr, b => b = CHAMBER_STATE.EMPTY);
        barrelPosition = 0;
        gatePosition = 1;
    }

    void Start()
    {
        sack = BulletSackController.Instance;
        chamberImage = GameObject.Find("ChamberImage").GetComponent<Image>();
        chamberDecal = GameObject.Find("FiredDecal");
        player = GameObject.Find("Player");
        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        textBox = GameObject.Find("ChamberStatusText").GetComponent<TMP_Text>();
        projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");

        setChamberEmpty();
    }


    public void setChamberEmpty()
    {
        textBox.SetText("Unloaded");
        chamberDecal.SetActive(false);
        chamberImage.sprite = chamberEmpty;
    }

    public void setChamberLoaded()
    {
        textBox.SetText("Loaded");
        chamberDecal.SetActive(false);
        chamberImage.sprite = chamberFull;
    }

    public void setChamberFired()
    {
        textBox.SetText("Spent");
        chamberDecal.SetActive(true);
        chamberImage.sprite = chamberFull;
    }

    public void cycleCylinder()
    {
        barrelPosition = wrapDecrement(barrelPosition);
        gatePosition = wrapDecrement(gatePosition);

        switch (cylinderStateArr[gatePosition])
        {
            case CHAMBER_STATE.EMPTY:
                setChamberEmpty();
                break;
            case CHAMBER_STATE.FIRED:
                setChamberFired();
                break;
            case CHAMBER_STATE.LOADED:
                setChamberLoaded();
                break;
        }
    }

    private int wrapDecrement(int toWrap)
    {
        return (toWrap == 0) ? CYLINDER_SIZE - 1 : toWrap - 1;
    }

    public void handleEject()
    {
        if (cylinderStateArr[gatePosition] == CHAMBER_STATE.LOADED)
        {
            sack.addItem(cylinderArr[gatePosition]);
        }
        cylinderArr[gatePosition] = null;
        cylinderStateArr[gatePosition] = CHAMBER_STATE.EMPTY;
        setChamberEmpty();
    }

    public void handleInsert()
    {
        if(cylinderStateArr[gatePosition] == CHAMBER_STATE.EMPTY && sack.getSize() > 0)
        {
            cylinderArr[gatePosition] = sack.getBullet();
            cylinderStateArr[gatePosition] = CHAMBER_STATE.LOADED;
            setChamberLoaded();
        }
    }

    public void handleFire()
    {
        if(cylinderStateArr[barrelPosition] == CHAMBER_STATE.LOADED)
        {
            Vector3 cameraPos = playerCamera.gameObject.transform.position;
            cBullet b = cylinderArr[barrelPosition];

            if (cylinderArr[barrelPosition].PrimerType is cBullet.PRIMER_TYPE.HEALING)
            {
                PlayerHealthManager.Instance.Heal(); // Heal the player for 1 health
            }

            // HANDLE BULLET FIRING LOGIC
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitPoint;
            Vector3 targetPosition;

            if(Physics.Raycast(ray, out hitPoint)){
                targetPosition = hitPoint.point;
            } else {
                targetPosition = ray.GetPoint(60);
            }

            Vector3 direction = targetPosition - cameraPos;

            GameObject proj = Instantiate(projectilePrefab, cameraPos, Quaternion.identity);
            proj.gameObject.GetComponent<ProjectileFireBehaviour>().setBullet(b);

            proj.transform.forward = direction.normalized;
            Rigidbody rb = proj.GetComponent<Rigidbody>();
            rb.AddForce(direction.normalized * b.ProjSpeed, ForceMode.Impulse);
            player.GetComponent<Rigidbody>().AddForce(-direction.normalized * b.Knockback, ForceMode.Impulse);
            HealthCylinderController.Instance.spin(b.ProjSpeed);
            cylinderStateArr[barrelPosition] = CHAMBER_STATE.FIRED;
            setChamberFired();
        }
    }

    public cBullet getChamber(){
        return cylinderArr[barrelPosition];
    }

    public CHAMBER_STATE getBarrelState()
    {
        return cylinderStateArr[barrelPosition];
    }
}
