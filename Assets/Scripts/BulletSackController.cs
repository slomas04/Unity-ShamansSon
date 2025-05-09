using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;

public class BulletSackController : MonoBehaviour
{
    public static BulletSackController Instance { get; private set; }

    [SerializeField] private const int MAX_SIZE = 255;
    [SerializeField] private const int MAX_RECUR = 10;

    [SerializeField] private Stack<cBullet> bStack;
    [SerializeField] private TMP_Text textBox;

    private System.Random r;

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        Instance = this;
        bStack = new Stack<cBullet>();
        r = new System.Random();
    }

    void Start()
    {
        textBox = gameObject.GetComponentInChildren<TMP_Text>();
    }


    void Update()
    {
        textBox.text = bStack.Count.ToString();
    }

    public bool addItem(cBullet b)
    {
        bool canPush = bStack.Count <= MAX_SIZE;
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
        if (i < MAX_RECUR && bStack.Count > 1 && r.NextDouble() < 0.5)
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

    public void resetBag(){
        bStack = new Stack<cBullet>();
    }
}
