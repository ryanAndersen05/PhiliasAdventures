﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is strictly for ground colliders. This will provide the proper height that the player should be at when they are touching the ground
/// </summary>
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
    #endregion main variables


    #region monobehaviour methods
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

    
}
