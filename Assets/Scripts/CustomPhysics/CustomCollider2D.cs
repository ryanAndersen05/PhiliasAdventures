using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script will rake the place of Unity Physics. Use this script on non static objects.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CustomPhysics2D))]

public class CustomCollider2D : MonoBehaviour {
    #region main variables
    [Tooltip("The number of ray traces that will be shot between the LEFT and RIGHT corners of our box collider")]
    public int verticalRayTraceCount = 2;
    [Tooltip("The number of ray traces that will be shot between the TOP and BOTTOM corners of our box collider")]
    public int horizontalRayTraceCount = 2;

    [Tooltip("This value is used to calculate how much the vertical rays will be pushed into the collider. If you would like them to be pushed out instead, set the value as negative.")]
    public float verticalOffset = .005f;
    [Tooltip("This value is used to calculate how much the horizontal rays will be pushed into the collider. If you would like them to be pushed out instead, set the value as negative.")]
    public float horizontalOffset = .005f;
    [Tooltip("The layers that are able to intereact with the players collider.")]
    public string[] layerMask;


    private Corners allCorners;
    private CustomPhysics2D rigid;
    private BoxCollider2D physicsCollider;
    #endregion main variables

    #region monobehaviour methods
    protected void Start()
    {
        physicsCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<CustomPhysics2D>();
    }

    protected virtual void Update()
    {
        updateCorners();
        checkVerticalRays();
        checkHorizontalRays();
    }

    protected virtual void OnValidate()
    {
        if (horizontalRayTraceCount < 2) horizontalRayTraceCount = 2;
        if (verticalRayTraceCount < 2) verticalRayTraceCount = 2;
    }
    #endregion monobehaviour methods

    #region collision methods
    /// <summary>
    /// Will fire off rays in the direction that it is traveling
    /// </summary>
    protected virtual void checkVerticalRays()
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
        rigid.inAir = true;
        for (int i = 0; i < verticalRayTraceCount; i++)
        {
            ray.origin = left + ((right - left) / (verticalRayTraceCount - 1)) * i;
            ray.direction = rigid.velocity.y <= 0 ? Vector2.down : Vector2.up;
            RaycastHit2D hit;
            float distanctToCalculate = Mathf.Abs(rigid.velocity.y) * Time.deltaTime + (rigid.velocity.y == 0f ? Mathf.Abs(verticalOffset) * 2 + .01f : 0);
            hit = Physics2D.Raycast(ray.origin, ray.direction, distanctToCalculate, LayerMask.GetMask(layerMask));
            if (hit)
            {
                //print("I hit a thing");
                GroundColliders groundCollider = hit.collider.GetComponent<GroundColliders>();
                if (groundCollider)
                {
                    Vector2 collisionPoint;
                    if (rigid.velocity.y <= 0)
                    {
                        collisionPoint = groundCollider.getTopPosition(transform.position.x);
                        transform.position = new Vector3(collisionPoint.x, collisionPoint.y, transform.position.z);
                    }
                    else
                    {
                        collisionPoint = groundCollider.getBottomPosition(transform.position.x);
                    }

                    rigid.inAir = false;
                    rigid.velocity.y = 0;
                }
            }

            if (DebugSettings.Instance.displayColliderRays)
            {
                DebugSettings.DrawDebugRay(ray.origin, ray.origin + (ray.direction * distanctToCalculate));
                //DebugSettings.DrawDebugRay(ray.origin, ray.origin + Vector2.down * .2f);
            }
        }
        //print(rigid.inAir);
    }

    protected virtual void checkHorizontalRays()
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
                GroundColliders groundCollider = hit.collider.GetComponent<GroundColliders>();
                if (groundCollider)
                {
                    Vector2 collisionPoint;
                    if ((rigid.velocity.x) < 0)
                    {
                        collisionPoint = groundCollider.getRightPosition(transform.position.y);
                        collisionPoint = collisionPoint + new Vector2(transform.position.x - allCorners.bottomLeft.x, 0);
                    }
                    else
                    {
                        collisionPoint = groundCollider.getLeftPosition(transform.position.y);
                        collisionPoint = collisionPoint - new Vector2(transform.position.x - allCorners.bottomRight.x, 0);
                    }
                    print(collisionPoint);
                    transform.position = new Vector3(collisionPoint.x, collisionPoint.y, transform.position.z);
                    rigid.velocity.x = 0;//If we hit a wall, then more than likely we will be setting the velocity to 0
                }
            }
            if (DebugSettings.Instance.displayColliderRays)
            {
                DebugSettings.DrawDebugRay(ray.origin, ray.origin + ray.direction * Mathf.Abs(xVel) * Time.deltaTime);
            }
        }
    }
    #endregion collision methods

    protected void updateCorners()
    {
        Corners newCorners = new Corners();
        newCorners.bottomLeft = new Vector2(physicsCollider.bounds.min.x, physicsCollider.bounds.min.y + .005f);
        newCorners.bottomRight = new Vector2(physicsCollider.bounds.max.x, physicsCollider.bounds.min.y + .005f);
        newCorners.topLeft = new Vector2(physicsCollider.bounds.min.x, physicsCollider.bounds.max.y);
        newCorners.topRight = physicsCollider.bounds.max;
        allCorners = newCorners;
    }

    #region structs
    /// <summary>
    /// This struct is used to store the four corners of our collision box
    /// </summary>
    protected struct Corners
    {
        public Vector2 topLeft;
        public Vector2 topRight;
        public Vector2 bottomLeft;
        public Vector2 bottomRight;
    }
    #endregion structs
}
