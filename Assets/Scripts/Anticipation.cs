using System.Collections;
using UnityEngine;

public class Anticipation : MonoBehaviour
{
    public float slowedTimeScale = 0.1f;
    public float slowDuration = 0.25f;
    
    void OnJump()
    {
        StartCoroutine(Anticipate());
    }

    private IEnumerator Anticipate()
    {
        Time.timeScale = slowedTimeScale;

        yield return new WaitForSecondsRealtime(slowDuration);

        Time.timeScale = 1.0f;
    }
    
}
