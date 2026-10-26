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
            lastMoveDirection = Vector3.zero;
            return;
        }

        newMoveDirection.Normalize();

        if (lastMoveDirection != Vector3.zero && Vector3.Dot(lastMoveDirection, newMoveDirection) < 0.99f)
        {
            PlayDirectionChangeParticles(newMoveDirection);
        }

        lastMoveDirection = newMoveDirection;
    }

    private void PlayDirectionChangeParticles(Vector3 moveDirection)
    {
        if (changeDirection == null)
        {
            return;
        }

        changeDirection.transform.rotation = Quaternion.LookRotation(-moveDirection, Vector3.up);
        changeDirection.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        changeDirection.Play(true);
    }
}