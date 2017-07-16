using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is strictly for ground colliders. This will provide the proper height that the player should be at when they are touching the ground
/// </summary>

[RequireComponent(typeof(Collider2D))]

public class GroundColliders : MonoBehaviour {
    #region enum values
    public enum GroundType
    {
        WALL,
        FLAT_GROUND,
        ANGLED_GROUND,
        ONE_WAY_PLATFORM
    }
    #endregion enum values

    #region main variables
    public GroundType groundType;

    private Collider2D mainCollider;
    #endregion main variables


    #region monobehaviour methods
    private void Start()
    {
        mainCollider = GetComponent<Collider2D>();
    }

    private void OnValidate()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        if (colliders.Length > 1)
        {
            for (int i = 1; i < colliders.Length; i++)
            {
                StartCoroutine(DestroyOnValidate(colliders[i]));//We really only want to focus on one collider per ground tile. Destroy all others if they have been added
            }
        }
        Collider2D collider = GetComponent<Collider2D>();
        switch (groundType)
        {
            case GroundType.WALL:
                if (!collider)
                {
                    this.gameObject.AddComponent<BoxCollider2D>();
                }
                else if (!(collider is BoxCollider2D))
                {
                    StartCoroutine(DestroyOnValidate(collider));
                    this.gameObject.AddComponent<BoxCollider2D>();
                }
                break;
            case GroundType.ANGLED_GROUND:
            case GroundType.FLAT_GROUND:
            case GroundType.ONE_WAY_PLATFORM:
                if (!collider)
                {
                    this.gameObject.AddComponent<EdgeCollider2D>();
                }
                else if (!(collider is EdgeCollider2D))
                {
                    StartCoroutine(DestroyOnValidate(collider));
                    this.gameObject.AddComponent<EdgeCollider2D>();
                }
                break;
        }
    }
    #endregion monobehaviour methods

    private IEnumerator DestroyOnValidate(Collider2D obj)
    {
        yield return new WaitForSeconds(.001f);
        DestroyImmediate(obj);
    }

    #region calculate position
    /// <summary>
    /// Top position is referring to the Top most portion of the ground tile given an x-Position
    /// This point is extrapolated and is based entirely on the angle of the tile. Make sure you are actually colliding with this tile before using this method
    /// </summary>
    /// <param name="xPosition"></param>
    /// <returns></returns>
    public Vector2 GetTopPosition(float xPosition)
    {
        switch (groundType)
        {
            case GroundType.FLAT_GROUND:
            case GroundType.ONE_WAY_PLATFORM:
            case GroundType.ANGLED_GROUND:

                break;

                
        }
        return Vector2.zero;
    }

    /// <summary>
    /// Bottom position is reffering the to the bottom most position of the ground tile given an x-Position
    /// This point is extrapolated and is based entirely on the angle of the tile. Make sure you are actually colliding with this tile before using this method
    /// </summary>
    /// <param name="xPosition"></param>
    /// <returns></returns>
    public Vector2 GetBottomPosition(float xPosition)
    {
        switch (groundType)
        {
            
        }
        return Vector2.zero;
    }

    /// <summary>
    /// Right position is referring to the right most position of the ground tile given a y-Position
    /// This point is extrapolated and is based entirely on the angle of the tile. Make sure you are actually colliding with this tile before using this method
    /// </summary>
    /// <param name="yPosition"></param>
    /// <returns></returns>
    public Vector2 GetRightPosition(float yPosition)
    {
        switch(groundType)
        {

        }
        return Vector2.zero;
    }

    /// <summary>
    /// Left position is referring to the left most position of the ground tile given a y-Positions.
    /// This point is extrapolated and is based entirely on the angle of the tile. Make sure you are actually colliding with this tile before using this method
    /// </summary>
    /// <param name="yPosition"></param>
    /// <returns></returns>
    public Vector2 GetLeftPosition(float yPosition)
    {
        switch (groundType)
        {

        }
        return Vector2.zero;
    }

    /// <summary>
    /// Use this method to find a position between two points given an x position. It wil take the angle of the points and interpolate it from the v1 position
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    private Vector2 FindPointBetweenX(Vector2 v1, Vector2 v2, float x)
    {
        Vector2 point;
        Vector2 dir = (v2 - v1).normalized;
        point = v1 + dir * (x - v1.x);
        return point;
    }


    private Vector2 FindPointBetweenY(Vector2 v1, Vector2 v2, float y)
    {
        Vector2 point;
        Vector2 dir = (v2 - v1).normalized;
        point = v1 + dir * (y - v1.y);
        return point;
    }
    #endregion calculate position
}
