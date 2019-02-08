using Sirenix.OdinInspector;
using UnityEngine;

public class GM: MonoBehaviour
{
    public static GM Instance;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        
    }
}
