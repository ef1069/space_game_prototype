using UnityEngine;
using UnityEngine.InputSystem;

public class ParticleChangeDir : MonoBehaviour
{
    public ParticleSystem changeDirection;
    private float lastMoveDirection;

    private void OnMove(InputValue input)
    {
        float horizontalInput = input.Get<Vector2>().x;

        if (horizontalInput != 0f)
        {
            float currentDirection = Mathf.Sign(horizontalInput);

            if (lastMoveDirection != 0f && currentDirection != lastMoveDirection)
            {
                PlayChDirParticles();
            }
            lastMoveDirection = currentDirection;
        }
    }

    private void PlayChDirParticles()
    {
        if (changeDirection != null)
        {
            changeDirection.Stop();
            changeDirection.Play();
        }
    }
}
