using Sirenix.OdinInspector;
using UnityEngine;

public class TankHover : MonoBehaviour
{
    [BoxGroup("Hover Steetings",true,true), LabelText("Lift Force"),Range(.1f,5)]
    public float Lift;

    [BoxGroup("Hover Steetings", true, true), LabelText("Hover Height"), Range(0, 2)]
    public float height;

    [BoxGroup("Hover Steetings", true, true), LabelText("Tank's Rigidbody")]
    public Rigidbody rb;

    [BoxGroup("Hover Steetings", true, true), LabelText("Ground Layer")]
    public LayerMask layers;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        HoverCheck();
    }

    private void HoverCheck()
    {
        var hit = Physics.Raycast(transform.position, Vector3.down, height, layers);

        if(hit)
        {
            rb.AddForce(Vector3.up * Lift, ForceMode.Impulse);
        }
    }
}
