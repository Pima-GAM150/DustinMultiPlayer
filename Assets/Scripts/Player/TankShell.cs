using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;

public class TankShell : MonoBehaviour
{
    #region Variables

    [BoxGroup("Shell Settings"), LabelText("Shell forward velocity"), Range(10, 50)]
    public float Speed;

    [BoxGroup("Shell Settings"), LabelText("Shell rotation velocity"), Range(5, 25)]
    public float RotSpeed;

    [BoxGroup("Shell Settings"), LabelText("Life time of shell"), Range(1, 10)]
    public float LifeTime;

    #endregion

    private void Start()
    {
        StartCoroutine("LifeSpan");
    }

    private void Update()
    {
        var Rot = transform.forward * RotSpeed;

        var Pos = Vector3.zero * Speed * Time.deltaTime;

        transform.position += Pos;

        transform.localEulerAngles += Rot;
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
