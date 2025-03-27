using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;


public class RevolverFrameBehaviour : MonoBehaviour
{
    public enum REVOLVER_STATE { IDLE, IDLE_T, HALF, HALF_T, FULL, FULL_T, FIRE, 
        RELOAD_SHUT, RELOAD_OPEN, RELOAD_INSERT, RELOAD_EJECT, RELOAD_EJECT_F };

    private Sprite[] sprites;
    private Dictionary<REVOLVER_STATE, Sprite> spriteMap;
    private Image GOImage;

    private TimeSpan thumbDuration;
    private DateTime lastThumb;

    private DateTime lastEject;

    [SerializeField] private REVOLVER_STATE currentState;
    [SerializeField] private bool isReloading;
    [SerializeField] private double thumbTime = 0.4;

    private void Awake()
    {
        thumbDuration = TimeSpan.FromSeconds(thumbTime);

        lastThumb = DateTime.UtcNow;
        lastEject = DateTime.UtcNow;

        spriteMap = new Dictionary<REVOLVER_STATE, Sprite>();
        sprites = Resources.LoadAll<Sprite>("Sprites/Revolver");
        foreach(Sprite s in sprites)
        {
            Enum.TryParse(s.name.ToUpper(), out REVOLVER_STATE stateEnum);
            spriteMap.Add(stateEnum, s);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GOImage = gameObject.GetComponent<Image>();
        setFrame(REVOLVER_STATE.IDLE);
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReloading)
        {
            double sWheel = Input.GetAxis("Mouse ScrollWheel");
            if(sWheel != 0) handleMWheel(sWheel > 0);
            if (Input.GetMouseButton(0)) handleMouseDown();

            if (currentState == REVOLVER_STATE.HALF && Input.GetKeyDown(KeyCode.R))
            {
                isReloading = true;
                setFrame(REVOLVER_STATE.RELOAD_SHUT);
                FirearmPositionManager.instance.setReloadPosition();
            }

        } else
        {
            if (currentState == REVOLVER_STATE.RELOAD_SHUT && Input.GetKeyDown(KeyCode.R))
            {
                isReloading = false;
                setFrame(REVOLVER_STATE.HALF);
                FirearmPositionManager.instance.setFirePosition();
                lastEject = DateTime.UtcNow - thumbDuration;
            } else if(currentState != REVOLVER_STATE.RELOAD_SHUT)
            {
                if (Input.GetKeyDown(KeyCode.E) && currentState != REVOLVER_STATE.RELOAD_EJECT_F) currentState = REVOLVER_STATE.RELOAD_INSERT;
                if (Input.GetKeyUp(KeyCode.E))
                {
                    currentState = REVOLVER_STATE.RELOAD_OPEN;
                    handleBulletInsert();
                    lastEject = DateTime.UtcNow - thumbDuration;
                }

                if (Input.GetKeyDown(KeyCode.F) && currentState != REVOLVER_STATE.RELOAD_INSERT) currentState = REVOLVER_STATE.RELOAD_EJECT_F;
                if (Input.GetKeyUp(KeyCode.F))
                {
                    lastEject = DateTime.UtcNow;
                    currentState = REVOLVER_STATE.RELOAD_OPEN;
                    handleBulletEject();
                }
            }

            if (currentState != REVOLVER_STATE.RELOAD_INSERT && currentState != REVOLVER_STATE.RELOAD_EJECT_F)
            {
                if (Input.GetKeyDown(KeyCode.T)) handleGate();
            }
        }


        if (!isReloading && (DateTime.UtcNow - lastThumb < thumbDuration))
        {
            string thumbString = Enum.GetName(typeof(REVOLVER_STATE), currentState) + "_T";
            Enum.TryParse(thumbString, out REVOLVER_STATE thumb);
            GOImage.sprite = spriteMap[thumb];
        } else
        {
            if(currentState == REVOLVER_STATE.RELOAD_OPEN && DateTime.UtcNow - lastEject < thumbDuration)
            {
                GOImage.sprite = spriteMap[REVOLVER_STATE.RELOAD_EJECT];
            } else
            {
                GOImage.sprite = spriteMap[currentState];
            }
        }
    }

    public void setFrame(REVOLVER_STATE s)
    {
        currentState = s;
    }

    private void handleMWheel(bool isUp)
    {
        lastThumb = DateTime.UtcNow;
        switch (currentState)
        {
            case REVOLVER_STATE.IDLE:
                currentState = (isUp) ? currentState : REVOLVER_STATE.HALF;
                break;
            case REVOLVER_STATE.HALF:
                currentState = (isUp) ? REVOLVER_STATE.IDLE : REVOLVER_STATE.FULL;
                break;
            case REVOLVER_STATE.FULL:
                currentState = (isUp) ? REVOLVER_STATE.HALF : currentState;
                break;
            default:
                break;
        }
    }

    private void handleMouseDown()
    {
        if(currentState == REVOLVER_STATE.FULL)
        {
            currentState = REVOLVER_STATE.IDLE;
            lastThumb = DateTime.UtcNow - thumbDuration;
            // TODO: A BULLET HAS JUST FIRED!
        }
    }

    private void handleGate()
    {
        if (currentState == REVOLVER_STATE.RELOAD_SHUT)
        {
            currentState = REVOLVER_STATE.RELOAD_OPEN;
        } else if (currentState == REVOLVER_STATE.RELOAD_OPEN)
        {
            currentState = REVOLVER_STATE.RELOAD_SHUT;
        }
    }

    private void handleBulletInsert()
    {
        print("Bullet Inserted!");
        // TODO: INSERT A BULLET!
    }

    private void handleBulletEject()
    {
        print("Bullet Ejected!");
        // TODO: EJECT A BULLET!
    }
}
