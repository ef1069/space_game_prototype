using UnityEngine;

public class AnimatedJump : MonoBehaviour
{
    [SerializeField] private float stretchAmount = 1.5f;
    [SerializeField] private float ascentStretchDuration = 0.2f;
    [SerializeField] private float descentStretchDuration = 0.2f;
    [SerializeField] private float blendDuration = 0.12f;
    [SerializeField] private float verticalSpeedThreshold = 0.05f;

    private Rigidbody rb;
    private Vector3 normalScale;
    private Vector3 scaleVelocity;
    private float stretchTimer;
    private JumpPhase jumpPhase;

    private enum JumpPhase
    {
        None,
        Ascending,
        Descending
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        normalScale = transform.localScale;
    }

    private void Update()
    {
        if (jumpPhase != JumpPhase.None)
        {
            float verticalSpeed = rb.linearVelocity.y;
            if (jumpPhase == JumpPhase.Ascending && verticalSpeed < -verticalSpeedThreshold)
            {
                jumpPhase = JumpPhase.Descending;
                stretchTimer = descentStretchDuration;
            }

            stretchTimer = Mathf.Max(0f, stretchTimer - Time.deltaTime);
            if (jumpPhase == JumpPhase.Descending && stretchTimer <= 0f)
            {
                jumpPhase = JumpPhase.None;
            }
        }

        Vector3 targetScale = normalScale;
        if (stretchTimer > 0f)
        {
            float amount = Mathf.Max(1f, stretchAmount);
            float radialScale = 1f / Mathf.Sqrt(amount);
            targetScale = new Vector3(
                normalScale.x * radialScale,
                normalScale.y * amount,
                normalScale.z * radialScale);
        }

        float smoothTime = Mathf.Max(0.001f, blendDuration);
        transform.localScale = Vector3.SmoothDamp(
            transform.localScale,
            targetScale,
            ref scaleVelocity,
            smoothTime);
    }

    public void OnJump()
    {
        jumpPhase = JumpPhase.Ascending;
        stretchTimer = ascentStretchDuration;
    }
}
