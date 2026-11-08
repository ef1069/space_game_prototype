using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Rigidbody target;
    public float x_value;
    public float y_value;
    public float z_value;
    private Vector3 newPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        newPosition.x = target.position.x + x_value;
        newPosition.y = target.position.y + y_value;
        newPosition.z = target.position.z + z_value;
        transform.position = newPosition;
    }
}
