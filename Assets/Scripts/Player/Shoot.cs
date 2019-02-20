﻿using UnityEngine;
using Sirenix.OdinInspector;
using Photon.Pun;

public class Shoot : MonoBehaviourPun
{
    #region Variables
    [BoxGroup("Projectile",true,true)]
    public GameObject Shell;

    [BoxGroup("Projectile", true, true), LabelText("Spawn Location")]
    public Transform SpawnTarget;

    [BoxGroup("Projectile", true, true), LabelText("Player Color"),ReadOnly]
    public Material PlayerColor;
    #endregion

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
        Transform gunPivot = GetComponent<GunElevation>().Pivot;

        GameObject newShell = Instantiate (Shell, SpawnTarget.position, Quaternion.identity);

        newShell.transform.forward = gunPivot.forward;
        
        newShell.GetComponentInChildren<Renderer>().material = PlayerColor;
    }

}
