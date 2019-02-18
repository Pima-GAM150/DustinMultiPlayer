using Sirenix.OdinInspector;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Variables
    [BoxGroup("Camera Follow Settings",true,true), LabelText("Target")]
    public Vector3 PlayerPos;

    [BoxGroup("Camera Follow Settings", true, true), LabelText("Smoothing Speed")]
    public float Smoothing = 0.125f;

    [BoxGroup("Camera Follow Settings", true, true), LabelText("Distance From Player"), Range(0,25)]
    public float Offset;

    [BoxGroup("Camera Status", true, true), LabelText("Yaw being Added"), ReadOnly]
    public float Yaw;

    [BoxGroup("Camera Status", true, true), LabelText("Current Rotation"), ReadOnly]
    public Vector3 CurrentRotation;

    [BoxGroup("Camera Status", true, true), LabelText("Rotation Smooth Velocity"), ReadOnly]
    public Vector3 SmoothVel;
    #endregion

    private void Start()
    {
        PlayerPos = NetworkedObjects.Instance.Players[NetworkedObjects.Instance.Players.Count - 1].gameObject.transform.position;
    }

    private void Update()
    {
        Yaw += Input.GetAxis("Mouse X") *6.38f;

        CurrentRotation = Vector3.SmoothDamp(CurrentRotation, new Vector3(10,Yaw,0), ref SmoothVel, Smoothing);

        transform.eulerAngles = CurrentRotation;

        transform.position = PlayerPos - transform.forward * Offset;
    }
}
