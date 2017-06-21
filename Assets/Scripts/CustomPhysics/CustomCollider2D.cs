using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script will rake the place of Unity Physics. Use this script on non static objects
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CustomPhysics2D))]

public class CustomCollider2D : MonoBehaviour {
    #region main variables
    [Tooltip("The number of ray traces that will be shot between the LEFT and RIGHT corners of our box collider")]
    public int verticalRayTraceCount = 2;
    [Tooltip("The number of ray traces that will be shot between the TOP and BOTTOM corners of our box collider")]
    public int horizontalRayTraceCount = 2;

    public float verticalOffset = .005f;
    public float horizontalOffset = .005f;
    public string[] layerMask;

    private Corners allCorners;
    private CustomPhysics2D rigid;
    private BoxCollider2D collider;
    #endregion main variables

    #region monobehaviour methods
    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<CustomPhysics2D>();
    }

    private void LateUpdate()
    {
        updateCorners();
        checkVerticalRays();
        checkHorizontalRays();
    }

    private void OnValidate()
    {
        if (horizontalRayTraceCount < 2) horizontalRayTraceCount = 2;
        if (verticalRayTraceCount < 2) verticalRayTraceCount = 2;
    }
    #endregion monobehaviour methods

    /// <summary>
    /// Will fire off rays in the direction that it is traveling
    /// </summary>
    private void checkVerticalRays()
    {
        Vector2 left = allCorners.bottomLeft;
        Vector2 right = allCorners.bottomRight;
        if (rigid.velocity.y > 0)
        {
            left = allCorners.topLeft;
            right = allCorners.topRight;
        }
        left.x += verticalOffset;
        right.x -= verticalOffset;
        Ray2D ray = new Ray2D();
        float yVel = rigid.velocity.y;
        for (int i = 0; i < verticalRayTraceCount; i++)
        {
            ray.origin = left + ((right - left) / (verticalRayTraceCount - 1)) * i;
            ray.direction = (Vector2.up * yVel).normalized;
            RaycastHit2D hit;
            hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Abs(yVel) * Time.deltaTime, LayerMask.GetMask(layerMask));
            if (hit)
            {
                transform.position = new Vector3(transform.position.x, rigid.velocity.y <= 0 ? hit.collider.bounds.max.y : hit.collider.bounds.min.y, transform.position.z);
                rigid.velocity.y = 0;
            }


            if (DebugSettings.Instance.displayColliderRays)
            {
                DebugSettings.DrawDebugRay(ray.origin, ray.origin + (ray.direction * Mathf.Abs(yVel) * Time.deltaTime));
                //DebugSettings.DrawDebugRay(ray.origin, ray.origin + Vector2.down * .2f);
            }
        }
    }

    private void checkHorizontalRays()
    {
        Vector2 top = allCorners.topRight;
        Vector2 bottom = allCorners.bottomRight;
        float xVel = rigid.velocity.x;
        if (xVel < 0)
        {
            top = allCorners.topLeft;
            bottom = allCorners.bottomLeft;
        }
        top.y -= horizontalOffset;
        bottom.y += horizontalOffset;
        Ray2D ray = new Ray2D();

        for (int i = 0; i < horizontalRayTraceCount; i++)
        {
            ray.origin = bottom + ((top - bottom) / (horizontalRayTraceCount - 1)) * i;
            ray.direction = (Vector2.right * xVel).normalized;
            RaycastHit2D hit;
            hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Abs(xVel) * Time.deltaTime, LayerMask.GetMask(layerMask));
            if (hit)
            {

            }
            if (DebugSettings.Instance.displayColliderRays)
            {
                DebugSettings.DrawDebugRay(ray.origin, ray.origin + ray.direction * Mathf.Abs(xVel) * Time.deltaTime);
            }
        }
    }

    private void updateCorners()
    {
        Corners newCorners = new Corners();
        newCorners.bottomLeft = collider.bounds.min;
        newCorners.bottomRight = new Vector2(collider.bounds.max.x, collider.bounds.min.y);
        newCorners.topLeft = new Vector2(collider.bounds.min.x, collider.bounds.max.y);
        newCorners.topRight = collider.bounds.max;
        allCorners = newCorners;
    }

    #region structs
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
    #endregion structs
}
