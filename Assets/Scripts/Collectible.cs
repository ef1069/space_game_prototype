using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Collectible : MonoBehaviour
{
    private bool waitPastInitialCollision = false;
    private int scoreValue = 1;
    public Vector3 rotationSpeed = new Vector3(0, 100, 0);
    private int playerScore = 0;
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
                Destroy(gameObject);
                audioPlayer.Play();
                playerScore += scoreValue;
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
