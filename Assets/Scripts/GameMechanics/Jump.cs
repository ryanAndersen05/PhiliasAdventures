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
}
