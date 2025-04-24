using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BossAI : MonoBehaviour
{
    [Header("References")]
    public BossBehavior EnemyBehavior;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask Ground, Player;
    public Animator animator;
    
    [Header("Patrolling")]
    public Vector3 walkPoint;
    bool walkPointSet = false;
    public float walkPointRange;
    
    [Header("States")]
    public float rotateSpeed = 5.0f;
    public float sightRange, slamRange, attackRange;
    public bool playerInSightRange, playerInSlamRange, playerInAttackRange;
    private bool isSlamming = false;
    
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
            playerInSlamRange = Physics.CheckSphere(transform.position, slamRange, Player);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

            if (agent.enabled)
            {
                if (!playerInSightRange && !isSlamming) Patrolling();
                if (playerInSightRange && !playerInSlamRange && !isSlamming)  ChasePlayer();
                if (playerInSlamRange && !playerInAttackRange)   SlamPlayer();
                if (playerInAttackRange && !isSlamming)   AttackPlayer();
            }
        }
    }

    private void Patrolling()
    {
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
        animator.SetBool("Running", true);
    }

    private void AttackPlayer()
    {
        animator.SetBool("Running", false);
        
        walkPointSet = false; // stop patrolling
        agent.SetDestination(transform.position);
        
        // face the player
        Vector3 toPlayer = transform.position - player.position;
        toPlayer.y = 0; // constrain vertically
        toPlayer = -toPlayer; // flip 180
        Quaternion lookRotation = Quaternion.LookRotation(toPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

        EnemyBehavior.Attack();
    }
    
    private void SlamPlayer()
    {
        if (!isSlamming)
        {
            isSlamming = true;
            StartCoroutine(SlamRoutine());
        }
    }

    IEnumerator SlamRoutine()
    {
        animator.SetBool("Running", false);
        StartCoroutine(PauseAgent(3.0f));
        
        // face the player
        Vector3 toPlayer = transform.position - player.position;
        toPlayer.y = 0; // constrain vertically
        toPlayer = -toPlayer; // flip 180
        Quaternion lookRotation = Quaternion.LookRotation(toPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

        yield return new WaitForSeconds(3f);
        EnemyBehavior.Slam();
        yield return new WaitForSeconds(4f);
        
        isSlamming = false;
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    IEnumerator PauseAgent(float seconds)
    {
        walkPointSet = false; // stop patrolling
        agent.isStopped = true;
        agent.ResetPath();
        
        yield return new WaitForSeconds(seconds);
        
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, slamRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
