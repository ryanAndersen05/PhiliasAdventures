using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSettings : MonoBehaviour {
    #region static variables
    private static DebugSettings instance;

    public static DebugSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<DebugSettings>();
            }
            return instance;
        }
    }
    #endregion static variables

    #region main variables
    public bool displayColliderRays = false;
    #endregion main variables

    #region monobehaviour methods
    private void Awake()
    {
        instance = this;
    }
    #endregion monobehaviouir methods

    #region static methods
    public static void DrawDebugRay(Vector2 origin, Vector2 destination, Color color)
    {
        Debug.DrawLine(origin, destination, color);
    }

    public static void DrawDebugRay(Vector2 origin, Vector2 destination)
    {
        DrawDebugRay(origin, destination, Color.red);
    }
    #endregion static methods


}
