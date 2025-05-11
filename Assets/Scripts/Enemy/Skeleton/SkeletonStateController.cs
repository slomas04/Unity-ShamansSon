using System;
using Unity.Mathematics;
using UnityEngine;

public class SkeletonStateController : EnemyStateController
{
    protected override void Awake()
    {
        initialState = new skeleton_state_idle(this);
        base.Awake();
    }

    public void setIsWalking(bool isWalking)
    {
        anim.SetBool("isWalking", isWalking);
    }
}
