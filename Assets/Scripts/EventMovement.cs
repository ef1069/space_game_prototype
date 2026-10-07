using UnityEngine;
using UnityEngine.InputSystem;

public class EventMovement : MonoBehaviour
{
    protected Rigidbody rb;
    public float force = 10.0f;
    public float horizSpeed = 10.0f;
    protected Vector2 moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        Vector3 move = new Vector3(moveInput.x, 0.0f, moveInput.y);

        rb.MovePosition(transform.position + move * horizSpeed * Time.fixedDeltaTime);
    }

    void OnJump()
    {
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

    
}
