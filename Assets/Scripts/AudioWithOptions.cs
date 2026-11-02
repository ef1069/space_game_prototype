using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AudioWithOptions : MonoBehaviour
{
    private bool waitPastInitialCollision = false;
    private bool isMoving = false;
    private bool isJumping = false;
    private AudioSource audioPlayer;
    private Rigidbody playerRigidbody;
    private readonly HashSet<Collider> groundContacts = new HashSet<Collider>();
    public AudioClip jumpingSound;
    public AudioClip landingSound;
    public AudioClip moveSound;
    public AudioClip chDirSound;
    public AudioClip collectSound;
    const string JUMP = "jump";
    const string LAND = "land";
    const string MOVE = "move";
    const string CHDIR = "change direction";
    const string COLLECT = "collect";
    private Dictionary<string, AudioClip> sfx = new Dictionary<string,AudioClip>();

    IEnumerator Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        playerRigidbody = GetComponent<Rigidbody>();
        sfx.Add(JUMP, jumpingSound);
        sfx.Add(LAND, landingSound);
        sfx.Add(MOVE, moveSound);
        sfx.Add(CHDIR, chDirSound);
        sfx.Add(COLLECT, collectSound);
        yield return new WaitForSeconds(0.1f);
        waitPastInitialCollision = true;
    }

    void Update()
    {
        if (isMoving && IsGrounded && moveSound != null && !audioPlayer.isPlaying)
        {
            PlayMoveSound();
        }
        else if (!IsGrounded)
        {
            StopMoveSound();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        UpdateGroundContact(collision);
        if (waitPastInitialCollision) {
            PlaySound(sfx[LAND]);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        UpdateGroundContact(collision);
    }

    void OnCollisionExit(Collision collision)
    {
        groundContacts.Remove(collision.collider);
        if (!IsGrounded)
        {
            StopMoveSound();
        }
    }

    void OnJump()
    {
        if (IsGrounded)
        {
            isJumping = true;
            StopMoveSound();
        }

        PlayJumpSound();
    }

    void OnMove(InputValue input)
    {
        isMoving = input.Get<Vector2>().sqrMagnitude > 0f;

        if (isMoving && IsGrounded)
        {
            PlayMoveSound();
        }
        else
        {
            StopMoveSound();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            PlaySound(sfx[COLLECT]);
        }
    }
    public void PlayJumpSound()
    {
        PlaySound(sfx[JUMP]);
    }

    public void PlayMoveSound()
    {
        if (!IsGrounded)
        {
            return;
        }

        if (moveSound == null)
        {
            Debug.LogError("Assign a move sound to AudioWithOptions.", this);
            return;
        }

        if (audioPlayer.clip == moveSound && audioPlayer.isPlaying)
        {
            return;
        }

        audioPlayer.clip = sfx[MOVE];
        audioPlayer.loop = true;
        audioPlayer.Play();
    }

    private bool IsGrounded => !isJumping && groundContacts.Count > 0;

    private void UpdateGroundContact(Collision collision)
    {
        bool hasGroundContact = false;
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (collision.GetContact(i).normal.y > 0.5f)
            {
                hasGroundContact = true;
                break;
            }
        }

        if (hasGroundContact)
        {
            groundContacts.Add(collision.collider);
            if (playerRigidbody.linearVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
        else
        {
            groundContacts.Remove(collision.collider);
        }
    }

    private void StopMoveSound()
    {
        if (audioPlayer.clip == moveSound)
        {
            audioPlayer.Stop();
        }
    }

    private void PlaySound(AudioClip clip)
    {
        audioPlayer.Stop();
        audioPlayer.loop = false;
        audioPlayer.clip = clip;
        audioPlayer.Play();
    }
}
