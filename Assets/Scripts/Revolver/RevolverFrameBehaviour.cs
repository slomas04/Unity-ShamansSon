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

    [SerializeField] private REVOLVER_STATE currentState;
    [SerializeField] private bool isReloading;
    [SerializeField] private double thumbTime = 0.4;

    private void Awake()
    {
        thumbDuration = TimeSpan.FromSeconds(thumbTime);
        lastThumb = DateTime.UtcNow;

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
        } else
        {

        }
        
        if(!isReloading && (DateTime.UtcNow - lastThumb < thumbDuration))
        {
            string thumbString = Enum.GetName(typeof(REVOLVER_STATE), currentState) + "_T";
            Enum.TryParse(thumbString, out REVOLVER_STATE thumb);
            GOImage.sprite = spriteMap[thumb];
        } else
        {
            GOImage.sprite = spriteMap[currentState];

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
}
