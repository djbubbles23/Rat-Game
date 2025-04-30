//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class knockbackScript : MonoBehaviour
{
    Rigidbody rb;
    public float knockbackForce; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Ground")){
            AddKnockback(collision.transform.position - transform.position, knockbackForce);
        }
    }
    public void AddKnockback(Vector3 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
}
