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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("BulletCollisions"))
        {
            Debug.Log("Boomable");

            Instantiate(BoomVFX, this.transform.position, Quaternion.identity);

            if(collision.gameObject.tag =="Player")
            {
                Debug.Log("Player");

                collision.gameObject.GetComponentInParent<IDamageable>().TakeDamage();

                collision.gameObject.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
            }
        }

        Destroy(this.gameObject);
    }
}
