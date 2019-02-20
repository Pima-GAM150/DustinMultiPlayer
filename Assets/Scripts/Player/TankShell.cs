using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;

public class TankShell : MonoBehaviour
{
    #region Variables

        [BoxGroup("Shell Settings"), LabelText("Shell forward velocity"), Range(5, 25)]
        public float Speed;

        [BoxGroup("Shell Settings"), LabelText("Shell rotation velocity"), Range(5, 25)]
        public float RotSpeed;

        [BoxGroup("Shell Settings"), LabelText("Life time of shell"), Range(1, 10)]
        public float LifeTime;

        [BoxGroup("Shell Components"), LabelText("Phisics Rigidbody"), ReadOnly]
        public Rigidbody rb;

    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.isKinematic = true;

        StartCoroutine("LifeSpan");
    }

    
    private void Update()
    {
       // transform.Translate(transform.forward * Speed * Time.deltaTime);
    }

    IEnumerator LifeSpan()
    {
        while(true)
        {
            yield return new WaitForSeconds(LifeTime);

            Destroy(this.gameObject);
        }
    }


}
