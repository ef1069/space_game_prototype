using System.Collections;
using UnityEngine;

public class AudioOnCollision : MonoBehaviour
{
    private bool waitPastInitialCollision = false;
    private AudioSource audioPlayer;

    IEnumerator Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        yield return new WaitForSeconds(0.1f);
        waitPastInitialCollision = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (waitPastInitialCollision) {
            audioPlayer.Play();
        }
    }
}