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


}
