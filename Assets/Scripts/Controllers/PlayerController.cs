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
        controllerDictionary.Add(MOVEMENT, new ControlInfo { isAxis = true });
        controllerDictionary.Add(JUMP, new ControlInfo());
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        updateAllInputs();
    }
    #endregion monobehaviour methods

    private void updateAllInputs()
    {
        Dictionary<string, ControlInfo> updateDictionary = new Dictionary<string, ControlInfo>();
        ControlInfo controlInfo;
        foreach (string key in controllerDictionary.Keys)
        {
            controlInfo = controllerDictionary[key];
            if (controlInfo.isAxis)
            {
                controlInfo.axisValue = Input.GetAxis(key);
                controlInfo.axisValueRaw = Input.GetAxisRaw(key);
            }
            else
            {
                controlInfo.isDown = Input.GetButtonDown(key);
                controlInfo.isHeld = Input.GetButton(key);
                controlInfo.isReleased = Input.GetButtonUp(key);
            }
            updateDictionary.Add(key, controlInfo);
        }
        controllerDictionary = updateDictionary;
    }

    public bool isDown(string buttonName)
    {
        if (!controllerDictionary.ContainsKey(buttonName))
        {
            Debug.LogWarning("There is no button input set as " + buttonName);
            return false;
        }
        if (controllerDictionary[buttonName].isAxis) Debug.LogWarning("This controller input is set as an axis. Button inputs will not be updated by default");

        return controllerDictionary[buttonName].isDown;
    }

    public bool isHeld(string buttonName)
    {
        if (!controllerDictionary.ContainsKey(buttonName))
        {
            Debug.LogWarning("There is no button input set as " + buttonName);
            return false;
        }
        if (controllerDictionary[buttonName].isAxis) Debug.LogWarning("This controller input is set as an axis. Button inputs will not be updated by default");

        return controllerDictionary[buttonName].isHeld;
    }

    public bool isReleased(string buttonName)
    {
        if (!controllerDictionary.ContainsKey(buttonName))
        {
            Debug.LogWarning("There is no button input set as " + buttonName);
            return false;
        }
        if (controllerDictionary[buttonName].isAxis) Debug.LogWarning("This controller input is set as an axis. Button inputs will not be updated by default");

        return controllerDictionary[buttonName].isReleased;
    }

    public float getAxis(string axisName)
    {
        if (!controllerDictionary.ContainsKey(axisName))
        {
            Debug.LogWarning("There is no input set as " + axisName);
            return 0;
        }
        if (!controllerDictionary[axisName].isAxis) Debug.LogWarning("This controller input is not set as an axis. By default, axis values will not be updated");
        return controllerDictionary[axisName].axisValue;
    }

    public float getAxisRaw(string axisName)
    {
        if (!controllerDictionary.ContainsKey(axisName))
        {
            Debug.LogWarning("There is no input set as " + axisName);
            return 0;
        }
        if (!controllerDictionary[axisName].isAxis) Debug.LogWarning("This controller is not set as an axis. By default, axis values will not be updated");
        return controllerDictionary[axisName].axisValueRaw;
    }

    private struct ControlInfo
    {
        public bool isAxis;
        public bool isDown;
        public bool isHeld;
        public bool isReleased;
        public float axisValue;
        public float axisValueRaw;
        public float bufferButtonPress;
    }
}
