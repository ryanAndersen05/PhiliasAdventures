using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomPhysics2D))]
public class CharacterMovement : MonoBehaviour {

    #region main variables
    private const float MinimumInputThreshold = .15f;

    [Header("Ground Movement")]
    [Tooltip("The max movement speed when the character is walking")]
    public float walkSpeed = 2.5f;
    [Tooltip("The max movement speed when the character is running")]
    public float runSpeed = 7;
    [Tooltip("The acceleration of player movement while they are on the ground")]
    public float acceleration = 25f;

    [Header("Air Movement")]
    [Tooltip("The maximum movement of the player while they are airborne")]
    public float airSpeed = 7.5f;
    [Tooltip("The acceleration of the player while they are in the air")]
    public float airAcceleration = 10f;

    [Header("Orientation Variables")]
    [Tooltip("If active the sprite will change direction based on the player's input")]
    public bool flipSpriteActive = true;
    [Tooltip("The current direction of the sprite. If you would like a sprite to begin in a certain direction, you can toggle this value in the editor")]
    public bool isRight = true;

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

        updateSpriteOrientation();
        updateSpritesActualDirection();

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
        else if (Mathf.Abs(hInput) > MinimumInputThreshold)
        {
            goalSpeed = Mathf.Sign(hInput) * walkSpeed;
        }
        rigid.velocity.x = Mathf.MoveTowards(rigid.velocity.x, goalSpeed, Time.deltaTime * acceleration);
    }

    private void updateMovementSpeedInAir()
    {

    }

    #region orientation methods
    private void updateSpriteOrientation()
    {
        if (!flipSpriteActive) return;
        if (Mathf.Abs(hInput) < MinimumInputThreshold) return;
        this.isRight = Mathf.Sign(hInput) > 0;
    }

    private void updateSpritesActualDirection()
    {
        float direction = Mathf.Sign(transform.localScale.x);
        if (this.isRight && direction < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if (!this.isRight && direction > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    #endregion orientation methods
}
