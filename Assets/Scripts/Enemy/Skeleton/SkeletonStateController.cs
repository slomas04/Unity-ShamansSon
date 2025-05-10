using System;
using Unity.Mathematics;
using UnityEngine;

public class SkeletonStateController : SkeletonStateController
{
    [SerializeField] public AudioClip SkeletonClick { get; private set; }
    [SerializeField] public AudioClip SkeletonDie { get; private set; }
    [SerializeField] public AudioClip SkeletonShoot { get; private set; }
    [SerializeField] public AudioClip SkeletonStep { get; private set; }
    protected override void Awake()
    {
        initialState = new skeleton_state_idle(this);
        base.Awake();
    }
}
