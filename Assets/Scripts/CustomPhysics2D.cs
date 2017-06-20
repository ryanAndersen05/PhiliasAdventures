using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For consistency in our 2d space we will not be using FixedUpdate at any point in this project
/// This script also aims to take the place of the built in physics engine of Unity
/// </summary>
public class CustomPhysics2D : MonoBehaviour {
    #region main variables
    public const float GRAVITY = 9.8f;
    public float gravityScale = 1.0f;

    public Vector2 velocity;

    [SerializeField]
    private Vector2 gravityDirection = Vector2.down;
    #endregion main variables

    #region monobehaviour methods
    private void Start()
    {
        gravityDirection = gravityDirection.normalized;
    }

    private void Update()
    {
        updateVelocityGravity();
    }
    #endregion monobehaviour methods

    public void setGravityDirection(Vector2 direction)
    {
        this.gravityDirection = direction.normalized;
    }

    /// <summary>
    /// Updates the velocity based on the direction of the gravityDirection.
    /// </summary>
    private void updateVelocityGravity()
    {

    }
}
