using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]

public class PlayerController : MonoBehaviour {
    #region static variables
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (!instance) instance = GameObject.FindObjectOfType<PlayerController>();
            return instance;
        }
    }
    #endregion static variables

    #region const variables
    public const string MOVEMENT = "Horizontal";
    public const string JUMP = "Jump";

    #endregion const variables

    private Dictionary<string, ControlInfo> controllerDictionary = new Dictionary<string, ControlInfo>();

    #region Monobehaviour methods
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
    }

    private void Update()
    {
        
    }
    #endregion monobehaviour methods

    private struct ControlInfo
    {
        public bool isDown;
        public bool isHeld;
        public bool isReleased;
        public float axis;
        public float bufferButtonPress;
    }
}
