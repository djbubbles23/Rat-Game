using UnityEngine;

public class knockbackScript : MonoBehaviour
{
    Rigidbody rb;
    public float knockbackForce = 0.25f; 
    public float drag = 1f; // Drag value to slow down the object

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = drag; // Set the drag value on the Rigidbody
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            AddKnockback(collision.transform.position - transform.position, knockbackForce);
        }
    }

    public void AddKnockback(Vector3 direction, float force)
    {
        rb.AddForce(direction.normalized * force, ForceMode.Impulse);
    }
}
