using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ParticleMovement : MonoBehaviour
{
    public ParticleSystem movement;
    private bool grounded = true;

    private void Awake()
    {
        if (movement != null)
        {
            var main = movement.main;
            main.loop = true;
        }
    }

    private void OnJump()
    {
        StartCoroutine(StopParticles());
    }

    private void OnMove(InputValue input)
    {
        Vector2 inputDirection = input.Get<Vector2>();
        Vector3 newMoveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);

        if (movement == null)
        {
            return;
        }

        if (newMoveDirection.sqrMagnitude == 0f)
        {
            movement.Stop();
            return;
        }

        newMoveDirection.Normalize();
        movement.transform.rotation = Quaternion.LookRotation(-newMoveDirection, Vector3.up);
        if (!movement.isPlaying && grounded == true)
        {
            movement.Play();
        }
    }

    IEnumerator StopParticles()
    {
        movement.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        grounded = false;
        yield return new WaitForSeconds(1.2f);
        grounded = true;
    }
}
