using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomPhysics2D))]
public class Jump : MonoBehaviour {
    [Tooltip("Mark this true if you would like the player to have a double jump ability")]
    public bool hasDoubleJump = true;
    [Tooltip("The desired height that you would like the player to reach on their jump")]
    public float jumpHeight;
    [Tooltip("The desired time before the player reaches that height")]
    public float timeToHeight;
    [Tooltip("The Gravity multiplier that will be added to physics when the player is not currently holding the jump button")]
    public float gravityMultiplierFastFall = 1.3f;
    [Tooltip("There may be certain situations where we don't want to apply a gravity scale. In these situations set this check false")]
    public bool useGravityScale = true;

    private bool usedDoubleJump;
    private CustomPhysics2D rigid;
    public float jumpVelocity { get; private set; }

    private void Start()
    {
        rigid = GetComponent<CustomPhysics2D>();
        jumpVelocity = 2 * jumpHeight / timeToHeight;
        rigid.gravityScale = jumpVelocity / timeToHeight / CustomPhysics2D.GRAVITY;
    }

    private void Update()
    {
        //print(rigid.inAir);
        if (PlayerController.Instance.isDown(PlayerController.JUMP))
        {
            if (!rigid.inAir)
            {
                hasDoubleJump = true;
                jump();
            }
            else if (hasDoubleJump)
            {
                hasDoubleJump = false;
                jump();
            }
        }

        if (CheckApplyGravityScale())
        {
            rigid.baseGravityScale = this.gravityMultiplierFastFall;
        } 
        else
        {
            rigid.baseGravityScale = 1;
        }

    }

    private void OnValidate()
    {
        if (!rigid)
        {
            rigid = GetComponent<CustomPhysics2D>();
        }
        jumpVelocity = 2 * jumpHeight / timeToHeight;
        rigid.gravityScale = jumpVelocity / timeToHeight / CustomPhysics2D.GRAVITY;
        rigid.terminalVelocity = jumpVelocity * rigid.fallingMultiplier;
    }

    public void jump()
    {
        rigid.velocity.y = jumpVelocity;
    }

    private bool CheckApplyGravityScale()
    {
        if (rigid.velocity.y <= 0) return false;
        if (!useGravityScale) return false;
        if (Input.GetButton("Jump")) return false;
        return true;
    }
}
