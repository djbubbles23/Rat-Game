using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public float bounceHeight = 0.5f;
    public float bounceSpeed = 2f;
    public AudioClip pickupSound;
    [Range(0f, 1f)] public float pickupVolume = 1f; // Volume control

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        float newY = startPos.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
