using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class NetworkedObjects : MonoBehaviour
{
	#region Variables
	public static NetworkedObjects Instance;
	
	private int Seed;
	
	[BoxGroup("WorldData",true,true)]
	public BoxCollider World;

	[BoxGroup("WorldData", true, true)]
	public List<Vector3> SpawnPoints;

	[BoxGroup("WorldData", true, true),ReadOnly]
	public List<PhotonView> Players;

	[BoxGroup("WorldData", true, true), ReadOnly]
	public int index;

	public UnityEvent AddedAPlayer;
	#endregion

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;

			index = Players.Count;
		}
		else
		{
			Destroy(this);
		}
	}

	private void Start()
	{
		Vector3 spawnPos;

		if(PhotonNetwork.IsMasterClient)
		{
			Seed = DateTime.Now.Second + System.Threading.Thread.CurrentThread.GetHashCode();
		}

		if(SpawnPoints.Count <= 0)
		{
			 var xRange = UnityEngine.Random.Range(-World.bounds.extents.x, World.bounds.extents.x);
			 var zRange = UnityEngine.Random.Range(-World.bounds.extents.z, World.bounds.extents.z);

			 spawnPos = World.bounds.center + new Vector3(xRange, 5f, zRange);

		   // spawnPos = new Vector3(0, 0, 0);
		}
		else
		{
			spawnPos = SpawnPoints[index % SpawnPoints.Count];
		}

		PhotonNetwork.Instantiate("Player", spawnPos, Quaternion.identity);
	}
	private void Update()
	{
		CheckPlayerHealth();
	}

	public void AddPlayer(PhotonView player)
	{
		Players.Add(player);

		if(PhotonNetwork.IsMasterClient)
		{
			player.RPC("SetColor", RpcTarget.AllBuffered, Players.Count - 1);

			AddedAPlayer?.Invoke();
		}
	}
	
	public void RemovePlayer(PhotonView player)
	{
		Players.Remove(player);
	}

	private void CheckPlayerHealth()
	{
		if (!PhotonNetwork.IsMasterClient)
			return;
		
		foreach(PhotonView p in Players)
		{
			if (p.GetComponent<Player>().Health <= 0) 
			{
				p.RPC("ReSpawn", RpcTarget.All);
			}
		}
	}
}
