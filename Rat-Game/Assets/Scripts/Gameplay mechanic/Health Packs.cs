using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float healAmount = 25f; // Amount of HP restored
    public float rotationSpeed = 50f; // Rotation speed
    public float bounceHeight = 0.5f; // Bounce height
    public float bounceSpeed = 2f; // Bounce speed

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position; // Store initial position
    }

    private void Update()
    {
        // Rotate the health pack
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // Bounce up and down
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

   
    
}
