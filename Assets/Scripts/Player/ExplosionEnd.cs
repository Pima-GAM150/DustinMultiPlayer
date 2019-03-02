using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEnd : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("EndExplosion");
    }

    IEnumerator EndExplosion()
    {
        var temp = 2;
        while(temp>0)
        {
            yield return new WaitForSeconds(1);

            temp--;
        }

        Destroy(this.gameObject);
    }
    
}
