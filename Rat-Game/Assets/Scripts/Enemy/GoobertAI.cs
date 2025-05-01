using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class GoobertAI : MonoBehaviour
{
    [Header("References")]
    public GoobertBehavior EnemyBehavior;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask Ground, Player;
    // public Animator animator;
    
    [Header("Patrolling")]
    public Vector3 walkPoint;
    bool walkPointSet = false;
    public float walkPointRange;
    
    [Header("States")]
    public float rotateSpeed = 5.0f;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    
    private Collider agentCollider;

    private void Awake()
    {
        // find and set player reference 
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        
        // ignore collisions with player (causes bug where enemy flies backwards)
        agentCollider = GetComponent<Collider>();
        Physics.IgnoreCollision(agentCollider, player.GetComponent<Collider>(), true);
    }

    private void Update()
    {
        if (agent.enabled)
        {
            // Check if player is in range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

            if (!playerInSightRange && !playerInAttackRange && !EnemyBehavior.IsAttacking()) Patrolling();
            if (playerInSightRange && !playerInAttackRange  && !EnemyBehavior.IsAttacking())  ChasePlayer();
            if (playerInSightRange && playerInAttackRange)   AttackPlayer();
        }
    }

    private void Patrolling()
    {
        // animator.SetBool("Running", true);
        
        if (!walkPointSet) SearchWalkPoint();
        agent.SetDestination(walkPoint);
        
        // Reset walk point
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        // print(distanceToWalkPoint.magnitude);
        if (distanceToWalkPoint.magnitude < 2.0f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        int maxAttempts = 30;
        for (int i = 0; i <= maxAttempts; i++)
        {
            // Calculate random point in range
            float randomX = Random.Range(-walkPointRange, walkPointRange);
            float randomZ = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            
            // check point is on level
            if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
            {
                walkPointSet = true;
                return;
            }
        }
    }

    private void ChasePlayer()
    {
        walkPointSet = false; // stop patrolling
        agent.SetDestination(player.position);
        EnemyBehavior.ResetAttack();
        // animator.SetBool("Running", true);
    }

    private void AttackPlayer()
    {
        // animator.SetBool("Running", false);
        walkPointSet = false; // stop patrolling
        agent.SetDestination(transform.position);
        
        // face the player
        Vector3 toPlayer = transform.position - player.position;
        toPlayer.y = 0; // constrain vertically
        toPlayer = -toPlayer; // flip 180
        Quaternion lookRotation = Quaternion.LookRotation(toPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

        if (!EnemyBehavior.IsAttacking())
        {
            EnemyBehavior.Attack();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
