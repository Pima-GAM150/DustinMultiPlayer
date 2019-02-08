using Sirenix.OdinInspector;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [LabelText("Target")]
    public Transform PlayerPos;

    [LabelText("Smoothing Speed")]
    public float Smoothing = 0.125f;

    [LabelText("Distance From Player")]
    public Vector3 Offset;

    private void FixedUpdate()
    {
        var desiredPos = PlayerPos.position + Offset;

        var smoothedPos = Vector3.Lerp(transform.position, desiredPos, Smoothing);

        transform.position = smoothedPos;

        transform.LookAt(PlayerPos);
    }
}
