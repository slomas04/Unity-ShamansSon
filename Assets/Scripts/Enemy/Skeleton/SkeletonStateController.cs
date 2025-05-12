using System;
using Unity.Mathematics;
using UnityEngine;

// This class is responsible for controlling the state of the Skeleton enemy
// It inherits from the EnemyStateController class
public class SkeletonStateController : EnemyStateController
{
    // Establish the initial state of the skeleton before the skeleton starts
    protected override void Awake()
    {
        initialState = new skeleton_state_idle(this);
        base.Awake();
    }

    // This is used for controlling the waling animation of the skeleton
    // The skeleton will only stop walking when this is set to false
    public void setIsWalking(bool isWalking)
    {
        anim.SetBool("isWalking", isWalking);
    }
}
