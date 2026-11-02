using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Trampoline))]
public class TrampolineAudio : MonoBehaviour
{
    private AudioSource audioPlayer;
    private Trampoline trampoline;
    public AudioClip trampolineSound;

    void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        trampoline = GetComponent<Trampoline>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") ||
            collision.rigidbody == null ||
            !trampoline.IsTopContact(collision))
        {
            return;
        }

        if (trampolineSound == null)
        {
            Debug.LogError("Assign a trampoline sound to TrampolineAudio.", this);
            return;
        }

        audioPlayer.PlayOneShot(trampolineSound);
    }
}
