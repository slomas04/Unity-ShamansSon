using System;
using Unity.Mathematics;
using UnityEngine;

// This class is responsible for controlling the state of the Snake enemy
// It inherits from the EnemyStateController class
public class SnakeStateController : EnemyStateController
{
    // Establish the initial state of the snake before the snake starts
    protected override void Awake()
    {
        initialState = new snake_state_idle(this);
        base.Awake();
        
    }
}
