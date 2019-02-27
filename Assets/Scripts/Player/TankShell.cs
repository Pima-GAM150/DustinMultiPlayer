using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.VFX;
using System.Collections;

public class TankShell : MonoBehaviour
{
    #region Variables

        [BoxGroup("Shell Settings"),LabelText("Life time of shell"), Range(1, 10)]
        public float LifeTime;

        [BoxGroup("Shell Settings"), LabelText("Explosion VFX")]
        public GameObject BoomVFX;

    #endregion
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boom"))
        {
            Instantiate(BoomVFX, this.transform.position, Quaternion.identity);
        }
        
    }

}
