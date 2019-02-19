using Sirenix.OdinInspector;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Variables

        [BoxGroup("Camera Follow Settings",true,true), LabelText("Target")]
        public Transform PlayerPos;

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

        [BoxGroup("Camera Status", true, true), LabelText("Rotation Smooth Velocity"), ReadOnly]
        public bool HasaPlayer;

    #endregion

    private void Start()
    {
        HasaPlayer = false;

        NetworkedObjects.Instance.AddedAPlayer.AddListener(OnPlayerAdded);
    }

    private void OnPlayerAdded()
    {
        if (!HasaPlayer)
        {
            HasaPlayer = true;

            PlayerPos = NetworkedObjects.Instance.Players[NetworkedObjects.Instance.Players.Count - 1].transform;
        }
    }

    private void Update()
    {
        if(!HasaPlayer)
        {
            return;
        }

        Yaw += Input.GetAxis("Mouse X") *6.38f;

        CurrentRotation = Vector3.SmoothDamp(CurrentRotation, new Vector3(10,Yaw,0), ref SmoothVel, Smoothing);

        transform.eulerAngles = CurrentRotation;

        transform.position = PlayerPos.position - transform.forward * Offset;

        Debug.Log(PlayerPos.position);
    }

    private void OnDisable()
    {
        NetworkedObjects.Instance.AddedAPlayer.RemoveListener(OnPlayerAdded);
    }
}
