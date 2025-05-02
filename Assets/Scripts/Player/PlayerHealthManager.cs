using UnityEngine;
using System;


public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager Instance { get; private set; }

    [SerializeField] private int startingHealth = 6;
    [SerializeField] private int currentHealth;
    [SerializeField] private double iFrameRaw = 0.5;

    private TimeSpan iFrameTime;
    private DateTime lastHit;

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
        HealthCylinderController.Instance.setHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 1)
        {
            // TODO: Handle death here!
        }
    }

    public void Hit()
    {
        if (DateTime.UtcNow - lastHit > iFrameTime  && currentHealth > 0)
        {
            currentHealth -= 1;
            HealthCylinderController.Instance.setHealth(currentHealth);
        }
    }

    public void Heal()
    {
        if (currentHealth < 6)
        {
            currentHealth += 1;
            HealthCylinderController.Instance.setHealth(currentHealth);
        }
    }
}
