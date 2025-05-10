using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager Instance { get; private set; }

    [SerializeField] private int startingHealth = 6;
    [SerializeField] private int currentHealth;
    [SerializeField] private double iFrameRaw = 0.5;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip healSound;
    [SerializeField] private AudioClip deathSound;

    public bool IsDead {get; private set;}
    private GameObject deathOverlay;

    private TimeSpan iFrameTime;
    private DateTime lastHit;
    private Transform mainCamera;
    private Image damageOverlay;
    private float fadeSpeed = 5f;
    private float opacityMax = 0.3f;
    private bool damageTime = false;

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        Instance = this;
        currentHealth = startingHealth;

        iFrameTime = TimeSpan.FromSeconds(iFrameRaw);
        lastHit = DateTime.UtcNow;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main.transform;
        damageOverlay = GameObject.Find("DamageOverlay").GetComponent<Image>();
        damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, 0f);
        deathOverlay = GameObject.Find("DeathOverlay");
        deathOverlay.SetActive(false);

        HealthCylinderController.Instance.setHealth(currentHealth);
        enablePlayer();
    }

    public void enablePlayer(){
        mainCamera.position = new Vector3(mainCamera.position.x, 1f, mainCamera.position.z);
        IsDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        float currentAlpha = damageOverlay.color.a;
        if(!(currentAlpha == 0f && damageTime == false) || !(currentAlpha == opacityMax && damageTime == true)){
            int mult = (damageTime) ? 1 : -1;
            float newAlpha = currentAlpha + (mult * Time.deltaTime * fadeSpeed);
            if (newAlpha < 0f) newAlpha = 0f;
            if (newAlpha > opacityMax) newAlpha = opacityMax;
            damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, newAlpha);
        }

        if (currentHealth < 1 && !IsDead)
        {
            mainCamera.position = new Vector3(mainCamera.position.x, 0.25f, mainCamera.position.z);
            IsDead = true;
            audioSource.PlayOneShot(deathSound);
            damageTime = true;
            deathOverlay.SetActive(true);
        }
    }

    public void Hit()
    {
        if (DateTime.UtcNow - lastHit > iFrameTime  && currentHealth > 0)
        {
            currentHealth -= 1;
            HealthCylinderController.Instance.setHealth(currentHealth);
            audioSource.PlayOneShot(hitSound);
            damageTime = true;
            if (!IsDead) Invoke("noRed", 0.25f);
        }
    }

    public void Heal()
    {
        if (currentHealth < 6)
        {
            currentHealth += 1;
            HealthCylinderController.Instance.setHealth(currentHealth);
            audioSource.PlayOneShot(healSound);
        }
    }

    private void noRed(){
        damageTime = false;
    }

    public void respawn(){
        currentHealth = startingHealth;
        HealthCylinderController.Instance.setHealth(currentHealth);
        IsDead = false;
        damageTime = false;
        deathOverlay.SetActive(false);
    }

    public void killPlayer(){
        currentHealth = 0;
        HealthCylinderController.Instance.setHealth(currentHealth);
    }
}
