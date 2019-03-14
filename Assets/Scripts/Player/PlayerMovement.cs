using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

public class PlayerMovement : MonoBehaviourPun, IPunObservable
{
	#region Variables

		[LabelText("How Fast"), Range(2, 15)]
		public float Speed;

		[BoxGroup("Position Data", true, true)]
		public Transform Appearance;

		[BoxGroup("Position Data", true, true)]
		public Transform Target;

		[BoxGroup("Position Data", true, true), ReadOnly]
		public Vector3 LastSyncedPos;

	#endregion

	private void Update()
	{
		if(photonView.IsMine)
		{
			var x = Input.GetAxis("Horizontal") * Speed * Time.deltaTime; 
			var z = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
			if (x == 0 && z == 0)
			{
				GetComponentInChildren<Rigidbody>().velocity = new Vector3(0,GetComponentInChildren<Rigidbody>().velocity.y,0);
			}

			Target.Translate(x, 0, z);

			if (!NetworkedObjects.Instance.World.bounds.Contains(Target.position))
			{
				Target.position = NetworkedObjects.Instance.World.bounds.ClosestPoint(Target.position);
			}

			Appearance.position = Target.position;
		}
		else
		{
			Appearance.position = Vector3.Lerp(Appearance.position, Target.position, Speed * Time.deltaTime);
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			
			LastSyncedPos = Target.position;

			stream.SendNext(Target.position);
		}
		else
		{
			Target.position = (Vector3)stream.ReceiveNext();
		}
	}
}
