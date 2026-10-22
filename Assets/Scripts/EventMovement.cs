using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class EventMovement : MonoBehaviour
{
    protected Rigidbody rb;
    private Renderer mr;
    private Material originalMaterial;
    [SerializeField] private Material collectibleMaterial;
    public float force = 10.0f;
    public float horizSpeed = 10.0f;
    protected Vector2 moveInput;
    private bool canJump = true;
    public float jumpTime = 1.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
                mr = GetComponentInChildren<Renderer>();
        if (mr != null)
        {
            originalMaterial = mr.sharedMaterial;
        }
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            if (mr == null)
            {
                Debug.LogError("PlayerMovement could not find a Renderer on this object or its children.", this);
                return;
            }

            if (collectibleMaterial == null)
            {
                Debug.LogError("Assign a collectible material to PlayerMovement.", this);
                return;
            }

            StartCoroutine(ShowCollectibleEffect());
        }
    }

    IEnumerator ShowCollectibleEffect()
    {
        mr.material = collectibleMaterial;
        yield return new WaitForSeconds(1.5f);
        mr.material = originalMaterial;
    }
    
}
