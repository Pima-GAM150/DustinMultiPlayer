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
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        
    }
}
