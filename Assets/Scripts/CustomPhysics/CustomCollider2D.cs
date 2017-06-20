using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class CustomCollider2D : MonoBehaviour {
    #region main variables
    [Tooltip("The number of ray traces that will be shot between the LEFT and RIGHT corners of our box collider")]
    public int verticalRayTraceCount = 2;
    [Tooltip("The number of ray traces that will be shot between the TOP and BOTTOM corners of our box collider")]
    public int horizontalRayTraceCount = 2;
    #endregion main variables

    #region monobehaviour methods
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnValidate()
    {
        if (horizontalRayTraceCount < 2) horizontalRayTraceCount = 2;
        if (verticalRayTraceCount < 2) verticalRayTraceCount = 2;
    }
    #endregion monobehaviour methods

    /// <summary>
    /// This struct is used to store the four corners of our collision box
    /// </summary>
    private struct Corners
    {
        public Vector2 topLeft;
        public Vector2 topRight;
        public Vector2 bottomLeft;
        public Vector2 bottomRight;
    }
}
