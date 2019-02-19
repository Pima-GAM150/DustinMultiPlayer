using UnityEngine;
using Photon.Pun;

public class CameraDeactivation : MonoBehaviourPun
{
    private void Start()
    {
        if(!GetComponentInParent<PhotonView>().IsMine)
        {
            this.gameObject.SetActive(false);
        }
    }
}
