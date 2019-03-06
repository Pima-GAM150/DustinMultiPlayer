using Sirenix.OdinInspector;
using UnityEngine;
using Photon.Pun;

public class Death : MonoBehaviourPun
{
    [PunRPC]
    public void ReSpawn()
    {
        GetComponent<PlayerMovement>().Target.position = new Vector3(200,5,200);

        Player.Instance.Health = 100;
    }
}
