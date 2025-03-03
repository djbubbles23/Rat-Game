using System;
using System.Collections;
using UnityEngine;

public class EnemyTracking : MonoBehaviour
{
    public float maxFollowDistance = 2f;    // Distance from player to stop within
    public float speed = 3f;                // Movement speed
    public float rotationSpeed = 5f;        // Rotation speed
    public float attackRange = 2.2f;        // How far enemy can attack from player
    public float attackWindup = 0.5f;       // time before enemy attacks
    public EnemyBehavior EnemyBehavior;     // reference to enemy behavior script

    private bool isGrounded = true;         // whether enemy is on ground
    private bool canAttack = true;          // whether the enemy attack is on cooldown
    private GameObject target = null;       // object to track to

    private void Start()
    {
        // set enemy target to track player
        target = GameObject.FindWithTag("Player");

        if (!target)
        {
            Debug.LogError("No object with player tag found");
        }
    }

    private void Update()
    {
        if (target != null && isGrounded)
        {
            FollowTarget();
        }
    }

    private void FollowTarget()
    {
        // Get the direction toward the target
        Vector3 direction = (target.transform.position - transform.position).normalized;

        // Calculate the distance to the target
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance < attackRange && canAttack)
        {
            // begin attack windup
            StartCoroutine(StartAttacking());
        }

        // Move only if beyond maxFollowDistance
        if (distance > maxFollowDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }

        // Smoothly rotate toward the target
        direction.y = 0; // prevent enemy from looking up
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void SetFollowTarget(GameObject followTarget)
    {
        target = followTarget;
    }
    
    // check if grounded to prevent enemy from tracking player
    //when airborn
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    IEnumerator StartAttacking()
    {
        //Debug.Log("Starting attacking");
        canAttack = false;
        yield return new WaitForSeconds(attackWindup);
        
        // check enemy is still within attacking range
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance < attackRange)
        {
            //Debug.Log("Trying attack now!");
            EnemyBehavior.Attack();
        }
        
        canAttack = true;
    }
}