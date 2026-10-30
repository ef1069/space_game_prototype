using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWithOptions : MonoBehaviour
{
    private bool waitPastInitialCollision = false;
    private AudioSource audioPlayer;
    public AudioClip jumpingSound;
    public AudioClip landingSound;
    const string JUMP = "jump";
    const string LAND = "land";
    private Dictionary<string, AudioClip> sfx = new Dictionary<string,AudioClip>();

    IEnumerator Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        sfx.Add(JUMP, jumpingSound);
        sfx.Add(LAND, landingSound);
        yield return new WaitForSeconds(0.1f);
        waitPastInitialCollision = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (waitPastInitialCollision) {
            audioPlayer.Stop();
            audioPlayer.clip = sfx[LAND];
            audioPlayer.Play();
        }
    }
    void OnJump()
    {
        PlayJumpSound();
    }

    public void PlayJumpSound()
    {
        audioPlayer.Stop();
        audioPlayer.clip = sfx[JUMP];
        audioPlayer.Play();
    }
}
