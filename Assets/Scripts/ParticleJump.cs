using UnityEngine;
using System.Collections;

public class ParticleJump : MonoBehaviour
{
    public ParticleSystem particles;
    private bool canPlay = false;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        canPlay = true;
    }
    void OnCollisionEnter()
    {
        if (canPlay == true) {
            particles.Play();
        }
    }
}
