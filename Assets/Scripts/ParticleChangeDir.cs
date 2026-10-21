using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParticleChangeDir : MonoBehaviour
{
    public ParticleSystem changeDirection;
    private Vector3 lastMoveDirection;
    private void OnMove(InputValue input)
    {
        Vector2 inputDirection = input.Get<Vector2>();
        Vector3 newMoveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);

        if (newMoveDirection.sqrMagnitude == 0f)
        {
            return;
        }

        newMoveDirection.Normalize();

        if (lastMoveDirection != Vector3.zero && Vector3.Dot(lastMoveDirection, newMoveDirection) < 0f)
        {
            PlayChDirParticles(newMoveDirection);
        }

        lastMoveDirection = newMoveDirection;
    }

    private void PlayChDirParticles(Vector3 moveDirection)
    {
        if (changeDirection != null)
        {
            changeDirection.transform.rotation = Quaternion.LookRotation(-moveDirection, Vector3.up);
            changeDirection.Stop();
            changeDirection.Play();
        }
}

}