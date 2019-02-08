using UnityEngine;
using Sirenix.OdinInspector;
using Photon.Pun;

public class Shoot : MonoBehaviourPun
{
    [BoxGroup("Projectile",true,true)]
    public GameObject Shell;

    [BoxGroup("Projectile", true, true), LabelText("Spawn Location")]
    public Transform SpawnTarget;

    [BoxGroup("Projectile", true, true), LabelText("Player Color"),ReadOnly]
    public Material PlayerColor;

    private void Start()
    {
        PlayerColor = GetComponentInChildren<Renderer>().material;
    }

    private void Update()
    {
        if(!photonView.IsMine)
        {
            return;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            photonView.RPC("FireShell", RpcTarget.All);
        }
    }

    [PunRPC]
    public void FireShell()
    {
        GameObject newShell = Instantiate(Shell, SpawnTarget.position, Quaternion.identity);

        newShell.GetComponentInChildren<Renderer>().material = PlayerColor;
    }

}
