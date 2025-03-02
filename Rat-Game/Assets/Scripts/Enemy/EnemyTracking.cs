using System;
using UnityEngine;

public class EnemyTracking : MonoBehaviour
{
    public Transform target;                // Target to follow (e.g., player)
    public float maxFollowDistance = 2f;    // Distance from player to stop within
    public float speed = 3f;                // Movement speed
    public float rotationSpeed = 5f;        // Rotation speed
    
    private bool isGrounded = false;

    private void Update()
    {
        if (target != null)
        {
            FollowTarget();
        }
    }

    private void FollowTarget()
    {
        // Get the direction toward the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Calculate the distance to the target
        float distance = Vector3.Distance(target.position, transform.position);

        // Move only if beyond maxFollowDistance
        if (distance > maxFollowDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        // Smoothly rotate toward the target
        direction.y = 0; // prevent enemy from looking up
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void SetFollowTarget(Transform followTarget)
    {
        target = followTarget;
    }
}