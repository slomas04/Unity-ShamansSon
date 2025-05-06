using System;
using Unity.Mathematics;
using UnityEngine;

public class SkeletonStateController : EnemyStateController
{
    protected override void Awake()
    {
        initialState = new snake_state_idle(this);
        base.Awake();
    }
}
