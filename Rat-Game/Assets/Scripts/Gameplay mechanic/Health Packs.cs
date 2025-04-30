using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float rotationSpeed = 90f;      
    public float bounceHeight = 0.5f;      
    public float bounceSpeed = 2f;         
    public int healAmount = 25;
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

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats player = other.GetComponent<PlayerStats>();
            if (player != null)
            {
                player.Heal(healAmount);
            }
            Destroy(gameObject);
        }
    }
}


