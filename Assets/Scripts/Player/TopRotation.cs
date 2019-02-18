using UnityEngine;
using Sirenix.OdinInspector;
using Photon.Pun;

public class TopRotation : MonoBehaviourPun , IPunObservable
{
    #region Variables
    [BoxGroup("Top Rotation Settings", true, true), LabelText("Rotation Speed"), Range(2,15)]
    public float Speed;

    [BoxGroup("Top Rotation Settings", true, true), LabelText("Top Pivot")]
    public Transform Pivot;

    [BoxGroup("Current Top Settings", true, true), LabelText("Last Rotation Angle"), ReadOnly]
    public float LastSyncedAngle;

    [BoxGroup("Current Top Settings", true, true), LabelText("Target Rotation Angle"), ReadOnly]
    public float TargetAngle;

    [BoxGroup("Current Top Settings", true, true), LabelText("Appearance Rotation Angle"), ReadOnly]
    public float AppearanceAngle;

    [BoxGroup("Current Top Settings", true, true), LabelText("Mouse X Value"), ReadOnly]
    public float MouseX;

    [BoxGroup("Current Top Settings", true, true), LabelText("Yaw being Added"), ReadOnly]
    public float Yaw;

    #endregion

    private void Update()
    {
        if (photonView.IsMine)
        {
            Yaw = Input.GetAxis("Mouse X") * Speed;

            TargetAngle += Yaw;

            Pivot.localEulerAngles = new Vector3(0f, TargetAngle, 0f);
        }
        else
        {
            AppearanceAngle = Mathf.Lerp(AppearanceAngle, TargetAngle, Speed * Time.deltaTime);

            Pivot.localEulerAngles = new Vector3(0f, AppearanceAngle, 0f);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            if(LastSyncedAngle!=TargetAngle)
                LastSyncedAngle = TargetAngle;

            stream.SendNext(TargetAngle);
        }
        else
        {
            TargetAngle = (float)stream.ReceiveNext();
        }
    }
}
