using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;
using Photon.Pun;

public class TankShell : MonoBehaviour
{
	#region Variables

		[BoxGroup("Shell Settings"), LabelText("Explosion VFX")]
		public GameObject BoomVFX;

		[BoxGroup("Shell Settings"), LabelText("Owner")]
		public Player MyPlayer;

	#endregion

	private void Start()
	{
		StartCoroutine("BlowUp", 6);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("BulletCollisions"))
		{
			Instantiate(BoomVFX, this.transform.position, Quaternion.identity);

			if (collision.gameObject.tag == "Player")
			{
				collision.gameObject.GetComponentInParent<IDamageable>().TakeDamage();
				
				collision.gameObject.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;

				if (collision.gameObject.GetComponentInParent<Player>().Health == 0)
				{
					MyPlayer.OnPlayerKilled();
				}
			}
		}
		  
		Destroy(this.gameObject);
	}
	
	IEnumerator BlowUp (int delay)
	{
		while (delay>0)
		{
			yield return new WaitForSeconds(1);

			delay--;
		}

		Destroy(this.gameObject);
	}
}