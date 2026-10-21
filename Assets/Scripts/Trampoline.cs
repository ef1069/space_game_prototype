using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Trampoline : MonoBehaviour
{
    public float force;
    [SerializeField] private float topContactTolerance = 0.05f;

    private Collider trampolineCollider;

    private void Awake()
    {
        trampolineCollider = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") || collision.rigidbody == null)
        {
            return;
        }

        float topY = trampolineCollider.bounds.max.y;
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (collision.GetContact(i).point.y >= topY - topContactTolerance)
            {
                collision.rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
                return;
            }
        }
    }
}
