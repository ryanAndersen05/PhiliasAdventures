using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    #region main variables
    public Vector2 cameraOffset = Vector2.zero;
    public float timeToReachTarget = .5f;

    private Transform targetTransform;
    private Vector3 currentCameraVelocity;
    #endregion main variables

    #region monobehaviour methods
    private void Start()
    {
        targetTransform = transform.parent;
        if (targetTransform == null)
        {
            Debug.LogWarning("The camera gameObject should be attached to a transform that it will follow");
            this.enabled = false;
        }
        this.transform.parent = null;
    }

    private void LateUpdate()
    {
        //print(Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, targetTransform.position + new Vector3(cameraOffset.x, cameraOffset.y, transform.position.z), ref currentCameraVelocity, timeToReachTarget);
    }
    #endregion monobehaviour methods
}
