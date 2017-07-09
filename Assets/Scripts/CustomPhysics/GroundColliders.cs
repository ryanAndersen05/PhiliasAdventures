using System.Collections;
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
        switch (groundType)
        {
            case GroundType.WALL:

                break;
            case GroundType.ANGLED_GROUND:
            case GroundType.FLAT_GROUND:
            case GroundType.ONE_WAY_PLATFORM:

                break;
        }
    }
    #endregion monobehaviour methods

    
}
