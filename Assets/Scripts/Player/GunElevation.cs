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

    [BoxGroup("Barrel Settings Read Only", true, true), LabelText("Mouse Y"), ReadOnly]
    public float MouseY;

    [BoxGroup("Barrel Settings Read Only", true, true), LabelText("Target Elevation"), ReadOnly]
    public float TargetGunAngle;

    [BoxGroup("Barrel Settings Read Only", true, true), LabelText("Last Elevation"), ReadOnly]
    public float LastGunAngle;

    [BoxGroup("Barrel Settings Read Only", true, true), LabelText("Display Elevation"), ReadOnly]
    public float AppearanceGunAngle;

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
            
        }
        else
        {

        }
        
    }

}
