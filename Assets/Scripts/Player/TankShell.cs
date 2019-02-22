using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;

public class TankShell : MonoBehaviour
{
    #region Variables

        [LabelText("Life time of shell"), Range(1, 10)]
        public float LifeTime;

    #endregion

    private void Start()
    {
        StartCoroutine("LifeSpan");
    }
    
    IEnumerator LifeSpan()
    {
        while(true)
        {
            yield return new WaitForSeconds(LifeTime);

            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("BulletCollisions"))
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage();
        }
    }

}
