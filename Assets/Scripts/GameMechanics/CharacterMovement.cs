using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    #region main variables
    [Tooltip("The max movement speed when the character is walking")]
    public float walkSpeed;
    [Tooltip("The max movement speed when the character is running")]
    public float runspeed;


    private float hInput;
    #endregion main variables

    #region monobehaviour methods
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
    #endregion monobehaviour methods


    public void setHorizontalInput(float hInput)
    {
        this.hInput = hInput;
    }

    private void updateMovementSpeed()
    {

    }
}
