using UnityEngine;
using Sirenix.OdinInspector;

public class ShellRotate : MonoBehaviour
{
    [LabelText("Rotation Speed"), Range(0, 20)]
    public float RotSpeed;

    
    void Update()
    {
        transform.Rotate(Vector3.up, RotSpeed);
    }
}
