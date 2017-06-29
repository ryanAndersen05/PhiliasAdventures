using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomPhysics2D))]
public class CharacterMovement : MonoBehaviour {

    #region main variables
    [Tooltip("The max movement speed when the character is walking")]
    public float walkSpeed = 2.5f;
    [Tooltip("The max movement speed when the character is running")]
    public float runSpeed = 7;
    [Tooltip("The acceleration of player movement while they are on the ground")]
    public float acceleration = 25f;

    [Tooltip("The maximum movement of the player while they are airborne")]
    public float airSpeed = 7.5f;
    [Tooltip("The acceleration of the player while they are in the air")]
    public float airAcceleration = 10f;

    private float hInput;
    private CustomPhysics2D rigid;
    #endregion main variables

    #region monobehaviour methods
    private void Start()
    {
        rigid = GetComponent<CustomPhysics2D>();
    }

    private void Update()
    {
        hInput = PlayerController.Instance.getAxisRaw(PlayerController.MOVEMENT);
        updateMovementSpeed();
    }
    #endregion monobehaviour methods


    public void setHorizontalInput(float hInput)
    {
        this.hInput = hInput;
    }

    private void updateMovementSpeed()
    {
        float goalSpeed = 0;
        if (Mathf.Abs(hInput) > .75f)
        {
            goalSpeed = Mathf.Sign(hInput) * runSpeed;
        }
        else if (Mathf.Abs(hInput) > .15f)
        {
            goalSpeed = Mathf.Sign(hInput) * walkSpeed;
        }
        rigid.velocity.x = Mathf.MoveTowards(rigid.velocity.x, goalSpeed, Time.deltaTime * acceleration);
    }

    private void updateMovementSpeedInAir()
    {

    }
}
