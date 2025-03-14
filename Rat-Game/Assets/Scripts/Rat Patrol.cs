using UnityEngine;

public class RatPatrol : MonoBehaviour
{
    public float speed = 2f;
    public float maxDistance = 10f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Vector3.Distance(startPos, transform.position) >= maxDistance)
        {
            transform.Rotate(0, 180, 0);
            startPos = transform.position;
        }
    }
}
