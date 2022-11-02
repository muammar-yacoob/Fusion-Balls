using Fusion;
using UnityEngine;

public class RestartPosition: NetworkBehaviour
{
    private Vector3 initialPosition;
    private Rigidbody rb;

    public override void Spawned()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    public override void FixedUpdateNetwork()
    {
        Reposition();
    }

    private void Reposition()
    {
        if(rb == null) return;
        if (transform.position.y <= -50)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.position = initialPosition;
        }
    }
}