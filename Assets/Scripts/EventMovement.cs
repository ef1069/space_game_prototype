using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class EventMovement : MonoBehaviour
{
    protected Rigidbody rb;
    public float force = 10.0f;
    public float horizSpeed = 10.0f;
    protected Vector2 moveInput;
    private bool canJump = true;
    public float jumpTime = 1.2f;
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

    public void OnJump()
    {
        if (canJump == true) {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            canJump = false;
            StartCoroutine(ResetJump());
        }
    }

    private IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(jumpTime);
        canJump = true;
    }

    void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

    
}
