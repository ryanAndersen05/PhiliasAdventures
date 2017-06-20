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
    [Tooltip("The maximum velocity that will be applied by the gravity force. If the speed is greater, gravity will not be applied")]
    public float terminalVelocity = 10.0f;
    [Tooltip("This is the direction of the gravity force that will be applied to this object. Will always update to a unit vector")]
    [SerializeField]
    private Vector2 gravityDirection = Vector2.down;
    [HideInInspector]
    public Vector2 velocity;
    #endregion main variables

    #region monobehaviour methods
    private void Start()
    {
        gravityDirection = gravityDirection.normalized;
    }

    private void Update()
    {
        updateVelocityGravity();
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
        print(velocity);
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
        Vector2 compareVec = gravityDirection * terminalVelocity;
        float scale = Mathf.Sign(compareVec.x);

        if (scale * compareVec.x > scale * velocity.x)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, gravityDirection.x * terminalVelocity, Mathf.Abs(Time.deltaTime * gravityScale * gravityDirection.x * GRAVITY));
        }
        scale = Mathf.Sign(compareVec.y);
        if (scale * compareVec.y > scale * velocity.y)
        {
            print(gravityDirection.y * terminalVelocity);
            velocity.y = Mathf.MoveTowards(velocity.y, gravityDirection.y * terminalVelocity, Mathf.Abs(Time.deltaTime * gravityScale * gravityDirection.y * GRAVITY));
        }
    }
}
