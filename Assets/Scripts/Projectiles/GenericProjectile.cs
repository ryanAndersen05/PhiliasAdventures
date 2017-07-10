using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomPhysics2D))]
public class GenericProjectile : MonoBehaviour {

    #region main variables
    public float launchSpeed;
    public Vector2 launchDirection;

    private CustomPhysics2D rigid;
    #endregion main variables

    #region monobehaviour methods
    private void Awake()
    {
        rigid = GetComponent<CustomPhysics2D>();
    }

    /// <summary>
    /// When an arrow is spawned it will typically begin in the launched state.
    /// The arrow that the player sees before we launch does not use this script
    /// </summary>
    private void Start()
    {
        
    }
    #endregion monobehaviour methods

    private void launchArrow()
    {

    }

}
