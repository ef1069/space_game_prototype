using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private bool waitPastInitialCollision = false;
    public Vector3 rotationSpeed = new Vector3(0, 100, 0);
    public float bobHeight = 0.2f;
    public float bobSpeed = 2.0f;
    private Vector3 startPosition;
    [SerializeField] private GameObject audioPlayerObject;
    private AudioSource audioPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        audioPlayer = audioPlayerObject.GetComponent<AudioSource>();
        startPosition = transform.position;
        yield return new WaitForSeconds(0.5f);
        waitPastInitialCollision = true; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (waitPastInitialCollision){
            if (other.CompareTag("Player"))
            {
                FindAnyObjectByType<ScoreKeeper>().ScorePoint();
                Destroy(gameObject);
                audioPlayer.Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
        transform.position = startPosition + Vector3.up * Mathf.Sin(Time.time * bobSpeed) * bobHeight;
    }
}
