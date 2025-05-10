using System;
using Unity.Mathematics;
using UnityEngine;

public class SnakeStateController : SkeletonStateController
{
    [SerializeField] public AudioClip SnakeHiss { get; private set; }
    [SerializeField] public AudioClip SnakeClick { get; private set; }
    [SerializeField] public AudioClip SnakeDie { get; private set; }
    [SerializeField] public AudioClip SnakeFire { get; private set; }

    protected override void Awake()
    {
        initialState = new snake_state_idle(this);
        base.Awake();
        
    }
}
