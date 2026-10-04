using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) moveInput.y = 1f;
        if (Keyboard.current.sKey.isPressed) moveInput.y = -1f;

        if (Keyboard.current.aKey.isPressed) moveInput.x = -1f;
        if (Keyboard.current.dKey.isPressed) moveInput.x = 1f;

        Vector3 movement = transform.forward * moveInput.y + transform.right * moveInput.x;
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
}
