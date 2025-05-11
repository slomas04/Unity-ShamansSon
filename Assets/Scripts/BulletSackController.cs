using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;

public class BulletSackController : MonoBehaviour
{
    public static BulletSackController Instance { get; private set; }

    [SerializeField] private int maxSize = 255;
    [SerializeField] private int maxRecurrences = 10;
    [SerializeField] private int StartingBullets = 2;

    [SerializeField] private Stack<cBullet> bStack;
    [SerializeField] private TMP_Text textBox;

    private System.Random r;

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        Instance = this;
        newSack();
        r = new System.Random();
    }

    void Start()
    {
        textBox = gameObject.GetComponentInChildren<TMP_Text>();
    }

    // We want the player to start with some default bullets upon start or death
    public void newSack()
    {
        bStack = new Stack<cBullet>();
        for (int i = 0; i < StartingBullets; i++)
        {
            bStack.Push(cBullet.GetGenericBullet());
        }
    }


    void Update()
    {
        textBox.text = bStack.Count.ToString();
    }

    public bool addItem(cBullet b)
    {
        bool canPush = bStack.Count <= maxSize;
        if (canPush)
        {
            bStack.Push(b);
        }
        return canPush;
    }

    public cBullet getBullet()
    {
        return recurAndGrab(0);
    }

    private cBullet recurAndGrab(int i)
    {
        if (i < maxRecurrences && bStack.Count > 1 && r.NextDouble() < 0.5)
        {
            cBullet tempBullet = bStack.Pop();
            cBullet rBullet = recurAndGrab(i + 1);
            bStack.Push(tempBullet);
            return rBullet;
        }
        return bStack.Pop();
    }

    public int getSize()
    {
        return bStack.Count;
    }
}
