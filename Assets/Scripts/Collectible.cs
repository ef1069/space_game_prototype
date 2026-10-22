using UnityEngine;

public class Collectible : MonoBehaviour
{
    private int scoreValue = 1;
    public Vector3 rotationSpeed = new Vector3(0, 100, 0);
    private int playerScore = 0;
    public float bobHeight = 0.2f;
    public float bobSpeed = 2.0f;
    private Vector3 startPosition;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collected!");
            Destroy(gameObject);
            playerScore += scoreValue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
        transform.position = startPosition + Vector3.up * Mathf.Sin(Time.time * bobSpeed) * bobHeight;
    }
}
