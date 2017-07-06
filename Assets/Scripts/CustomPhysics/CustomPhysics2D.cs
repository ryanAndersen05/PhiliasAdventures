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
    
    [Tooltip("The scale of the gravity force that will be applied to the character")]
    public float gravityScale = 1.0f;
    [Tooltip("A multipler that will be applied to the gravity when the character is falling")]
    public float fallingMultiplier = 1.4f;

    [Tooltip("The maximum velocity that will be applied by the gravity force. If the speed is greater, gravity will not be applied")]
    public float terminalVelocity = 10.0f;
    [Tooltip("This is the direction of the gravity force that will be applied to this object. Will always update to a unit vector")]
    [SerializeField]
    private Vector2 gravityDirection = Vector2.down;
    [HideInInspector]
    public Vector2 velocity;
    [HideInInspector]
    public bool inAir;
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

    /// <summary>
    /// We want to move the player after everything has been calculates, so it will be placed in the LateUpdate for now
    /// </summary>
    private void LateUpdate()
    {
        updatePositionFromVelocity();
    }

    private void OnValidate()
    {
        setGravityDirection(gravityDirection);
        if (terminalVelocity < 0) terminalVelocity = 0;
    }
    #endregion monobehaviour methods

    private void updatePositionFromVelocity()
    {
        //print(velocity);
        Vector2 originalPosition = new Vector2(transform.position.x, transform.position.y);
        originalPosition += velocity * Time.deltaTime;
        transform.position = new Vector3(originalPosition.x, originalPosition.y, transform.position.z);
    }

    public void setGravityDirection(Vector2 direction)
    {
        this.gravityDirection = direction.normalized;
    }

    /// <summary>
    /// Updates the velocity based on the direction of the gravityDirection.
    /// </summary>
    private void updateVelocityGravity()
    {
        if (!inAir) return;
        Vector2 compareVec = gravityDirection * terminalVelocity;
        float scale = Mathf.Sign(compareVec.x);
        float actualGrav = gravityScale * (velocity.y < 0 ? fallingMultiplier : 1);

        if (scale * compareVec.x > scale * velocity.x)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, gravityDirection.x * terminalVelocity, Mathf.Abs(Time.deltaTime * actualGrav * gravityDirection.x * GRAVITY));
        }
        scale = Mathf.Sign(compareVec.y);
        if (scale * compareVec.y > scale * velocity.y)
        {
            //print(gravityDirection.y * terminalVelocity);
            velocity.y = Mathf.MoveTowards(velocity.y, gravityDirection.y * terminalVelocity, Mathf.Abs(Time.deltaTime * actualGrav * gravityDirection.y * GRAVITY));
        }
    }
}
