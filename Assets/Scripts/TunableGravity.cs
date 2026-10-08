using UnityEngine;

public class TunableGravity : MonoBehaviour
{
    private Rigidbody rb;
    private float gravity = 9.8f;
    public float upGravityMultiple = 1.0f;
    public float downGravityMultiple = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.linearVelocity.y > 0)
        {
            rb.AddForce(ApplyJumpingGravity(), ForceMode.Acceleration);
        }
        else if (rb.linearVelocity.y == 0)
        {
            rb.AddForce(Vector3.down * gravity);
        } 
        else
        {
            rb.AddForce(ApplyFallingGravity(), ForceMode.Acceleration);
        }
    }

    Vector3 ApplyJumpingGravity()
    {
        return Vector3.down * gravity * upGravityMultiple;
    }

    Vector3 ApplyFallingGravity()
    {
        return Vector3.down * gravity * downGravityMultiple;
    }
}
