using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RatAI : MonoBehaviour
{
    [Header("References")]
    public EnemyBehavior EnemyBehavior;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask Ground, Player;
    
    [Header("Patrolling")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    
    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (agent.enabled)
        {
            // Check if player is in range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            if (playerInSightRange && !playerInAttackRange)  ChasePlayer();
            if (playerInSightRange && playerInAttackRange)   AttackPlayer();
        }
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();
        agent.SetDestination(walkPoint);
        
        // Reset walk point
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        // check point is on level
        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
        {
            walkPointSet = true;
        }
        else
        {
            SearchWalkPoint();
        }
    }

    private void ChasePlayer()
    {
        walkPointSet = false; // stop patrolling
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        walkPointSet = false; // stop patrolling
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        EnemyBehavior.Attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
