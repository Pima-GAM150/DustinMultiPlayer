using Sirenix.OdinInspector;
using UnityEngine;
using Photon.Pun;

public class GunElevation : MonoBehaviourPun , IPunObservable
{
    [BoxGroup("Barrel Settings", true, true), LabelText("Max Elevation")]
    public float BarrelAngleMax = 50f;

    [BoxGroup("Barrel Settings", true, true), LabelText("Min Elevation")]
    public float BarrelAngleMin = -5f;

    [BoxGroup("Barrel Settings", true, true), LabelText("Barrel Pivot")]
    public Transform Pivot;

    [BoxGroup("Barrel Settings", true, true), LabelText("Elevation Change Speed"), Range(2,15)]
    public float Speed = -5f;

    [BoxGroup("Current Barrel Settings", true, true), LabelText("Mouse Y"), ReadOnly]
    public float MouseY;

    [BoxGroup("Current Barrel Settings", true, true), LabelText("Target Elevation"), ReadOnly]
    public float TargetGunAngle;

    [BoxGroup("Current Barrel Settings", true, true), LabelText("Last Elevation"), ReadOnly]
    public float LastGunAngle = 0f;

    [BoxGroup("Current Barrel Settings", true, true), LabelText("Display Elevation"), ReadOnly]
    public float AppearanceGunAngle = 0f;

    [BoxGroup("Current Barrel Settings", true, true), LabelText("Pitch Being Added"), ReadOnly]
    public float pitch;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
           if(LastGunAngle != TargetGunAngle)
            {
                LastGunAngle = TargetGunAngle;

                stream.SendNext(TargetGunAngle);
            }
        }
        else
        {
            TargetGunAngle = (float)stream.ReceiveNext();
        }
    }

    private void Update()
    {
        if(photonView.IsMine)
        {
            TargetGunAngle = Mathf.Clamp(TargetGunAngle - (Input.GetAxis("Mouse Y") * Speed), -BarrelAngleMax, -BarrelAngleMin);

            Pivot.localEulerAngles = new Vector3(Mathf.Clamp(TargetGunAngle, -BarrelAngleMax, -BarrelAngleMin), 0f, 0f);
        }
        else
        {
            var pitch = Mathf.Lerp(AppearanceGunAngle, TargetGunAngle, Speed * Time.deltaTime);

            Pivot.localEulerAngles = new Vector3(Mathf.Clamp(pitch, -BarrelAngleMax, -BarrelAngleMin), 0f, 0f);
        }
        
    }

}
