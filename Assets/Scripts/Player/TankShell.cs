using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.VFX;
using System.Collections;

public class TankShell : MonoBehaviour
{
	#region Variables

		[BoxGroup("Shell Settings"), LabelText("Explosion VFX")]
		public GameObject BoomVFX;

	#endregion
	private void Start()
	{
		StartCoroutine("BlowUp", 6);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == 1 << LayerMask.NameToLayer("Ground") || collision.gameObject.layer == 1 << LayerMask.NameToLayer("BulletCollisions"))
		{
			Debug.Log("Boomable");

			Instantiate(BoomVFX, this.transform.position, Quaternion.identity);

			if(collision.gameObject.tag =="Player")
			{
				Debug.Log("Player");

				collision.gameObject.GetComponentInParent<IDamageable>().TakeDamage();

				var players = FindObjectsOfType<Player>();

				foreach(Player p in players)
				{
					if(p.GetComponent<PlayerColor>().CurrentColor == this.GetComponentInChildren<Renderer>().material)
					{
						print("player Found");

						if (p.Health > 0)
						{
							p.OnPlayerKilled();
						}						
					}
				}
				
				collision.gameObject.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
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
