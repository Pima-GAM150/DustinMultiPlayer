using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

public class NetworkedObjects : MonoBehaviour
{
    public static NetworkedObjects Instance;
    
    private int Seed;

    [BoxGroup("WorldData",true,true)]
    public BoxCollider World;

    [BoxGroup("WorldData", true, true)]
    public List<Vector3> SpawnPoints;

    [ReadOnly]
    public List<PhotonView> Players;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        Vector3 spawnPos;

        if(PhotonNetwork.IsMasterClient)
        {
            Seed = DateTime.Now.Second + System.Threading.Thread.CurrentThread.GetHashCode();
        }

        if(SpawnPoints.Count<=0)
        {
            var xRange = UnityEngine.Random.Range(-World.bounds.extents.x, World.bounds.extents.x);
            var zRange = UnityEngine.Random.Range(-World.bounds.extents.z, World.bounds.extents.z);

            spawnPos = World.bounds.center + new Vector3(xRange, 1f, zRange); 
        }
        else
        {
            spawnPos = SpawnPoints[Players.Count-1];
        }

        PhotonNetwork.Instantiate("Player", spawnPos, Quaternion.identity, 0);
    }

    public void AddPlayer(PhotonView player)
    {
        Players.Add(player);

        if(PhotonNetwork.IsMasterClient)
        {
            player.RPC("SetColor",RpcTarget.AllBuffered,UnityEngine.Random.Range(0,19) );
        }
    }

}
