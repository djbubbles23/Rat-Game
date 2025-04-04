using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;            // Movement speed
    public float jumpForce = 7f;           // Jump strength
    public GameObject attackHitbox;        // Object that represents player attack hitbox
    private Rigidbody rb;                  // Rigidbody of the player gameobject
    private VisualEffect atc;              // VisualEffect component for player's attacks
    private Vector3 movement;              // Movement vector based on player inputs
    private bool isGrounded;               // State flag for whether the player is on the ground
    private bool jumpInput;                // State flag for jump input
    private bool attackInput;              // State flag for attack input
    private bool canAttack = true;         // State flag for whether the player can attack
    public float attackDelay = 1f;         // Delay between attacks in seconds
    private float attackCounter;           // Counter for attack delay
    private int facing;                    // Direction the player is facing

    public GameObject Weapon;

    // Animation State Machine
    private Animator playerAnim;

    private enum Direction
    {
        North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest
    }

    public void Awake()
    {
        playerAnim = GetComponent<Animator>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        atc = gameObject.GetComponentInChildren<VisualEffect>();
    }

void Update()
{
    CheckInputs();
<<<<<<< HEAD
<<<<<<< HEAD

    if (jumpInput && isGrounded)
    {
        Jump();
    }

<<<<<<< HEAD
        if (jumpInput && isGrounded)
        {
            Jump();
        }

        if (attackInput && canAttack && movement == Vector3.zero)
        {
            Attack();
        }
        else if (!canAttack)
        {
            attackCounter += Time.deltaTime;
            if (attackCounter >= attackDelay)
            {
                canAttack = true;
                attackCounter = 0;
            }
        }

        HandleMovementAnimations();
=======
=======

    if (jumpInput && isGrounded)
    {
        Jump();
    }

>>>>>>> parent of 1fdb671 (Revert "Merge branch 'main' into jacob-enemy")
    // Prevent attacking while moving
    if (attackInput && canAttack && movement == Vector3.zero)
    {
        Attack();
<<<<<<< HEAD
=======
    }
    else if (!canAttack)
    {
        attackCounter += Time.fixedDeltaTime;

        if (attackCounter >= attackDelay)
        {
            canAttack = true;
            attackCounter = 0;
        }
>>>>>>> parent of 1fdb671 (Revert "Merge branch 'main' into jacob-enemy")
    }
    else if (!canAttack)
    {
        attackCounter += Time.fixedDeltaTime;

        if (attackCounter >= attackDelay)
        {
            canAttack = true;
            attackCounter = 0;
        }
>>>>>>> parent of 1fdb671 (Revert "Merge branch 'main' into jacob-enemy")
    }

    Vector3 movementDirection = new Vector3(movement.x, 0, movement.z);
    float magniture = Mathf.Clamp01(movementDirection.magnitude) * moveSpeed;
    movementDirection.Normalize();
    if (movementDirection.magnitude > 0)
    {
        playerAnim.SetBool("isMoving", true);
    }
    else
    {
        playerAnim.SetBool("isMoving", false);
    }
}

=======

    if (jumpInput && isGrounded)
    {
        Jump();
    }

    // Prevent attacking while moving
    if (attackInput && canAttack && movement == Vector3.zero)
    {
        Attack();
    }
    else if (!canAttack)
    {
        attackCounter += Time.fixedDeltaTime;

        if (attackCounter >= attackDelay)
        {
            canAttack = true;
            attackCounter = 0;
        }
    }

>>>>>>> parent of 290f4b8 (Merge pull request #10 from djbubbles23/jacob-enemy)
    Vector3 movementDirection = new Vector3(movement.x, 0, movement.z);
    float magniture = Mathf.Clamp01(movementDirection.magnitude) * moveSpeed;
    movementDirection.Normalize();
    if (movementDirection.magnitude > 0)
    {
        playerAnim.SetBool("isMoving", true);
    }
    else
    {
        playerAnim.SetBool("isMoving", false);
    }
}

    void FixedUpdate()
    {
        DirectionCheck();
        MovePlayer();
        RotatePlayer();
    }

    void CheckInputs()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        jumpInput = Input.GetButtonDown("Jump");
        attackInput = Input.GetButtonDown("Fire1");
    }

    void MovePlayer()
    {
        Vector3 move = movement * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

    void Jump()
    {
        playerAnim.SetTrigger("isJumping");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void Attack()
    {
        canAttack = false;
        playerAnim.SetTrigger("isAttacking");
        StartCoroutine(ActivateAttackHb());
    }

    IEnumerator ActivateAttackHb()
    {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
=======
>>>>>>> parent of 1fdb671 (Revert "Merge branch 'main' into jacob-enemy")
=======
>>>>>>> parent of 290f4b8 (Merge pull request #10 from djbubbles23/jacob-enemy)
        //Debug.Log("Attacking t");
        playerAnim.SetBool("Attack1", true);
        // activate attack hitbox for 0.1 seconds
>>>>>>> parent of 1fdb671 (Revert "Merge branch 'main' into jacob-enemy")
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(0.2f); // Duration of the attack hitbox
        atc.Play();
        attackHitbox.SetActive(false);
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        yield return new WaitForSeconds(0.5f); // Wait for the attack animation to finish
        playerAnim.ResetTrigger("isAttacking");
=======
=======
>>>>>>> parent of 1fdb671 (Revert "Merge branch 'main' into jacob-enemy")
=======
>>>>>>> parent of 290f4b8 (Merge pull request #10 from djbubbles23/jacob-enemy)
        //Debug.Log("Attacking f");
        playerAnim.SetBool("Attack1", false);
>>>>>>> parent of 1fdb671 (Revert "Merge branch 'main' into jacob-enemy")
    }

    void HandleMovementAnimations()
    {
        Vector3 movementDirection = new Vector3(movement.x, 0, movement.z);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * moveSpeed;
        movementDirection.Normalize();

        if (magnitude > 0)
        {
            playerAnim.SetBool("isMoving", true);
        }
        else
        {
            playerAnim.SetBool("isMoving", false);
        }
    }

<<<<<<< HEAD
    void DirectionCheck()
    {
        if (movement.x == 0 && movement.z == 0)
        {
            return; // No movement, no need to update direction
        }

        if (movement.x > 0) // North
        {
            if (movement.z > 0)
                facing = (int)Direction.NorthEast;
            else if (movement.z < 0)
                facing = (int)Direction.NorthWest;
            else
                facing = (int)Direction.North;
        }
        else if (movement.x < 0) // South
        {
            if (movement.z > 0)
                facing = (int)Direction.SouthEast;
            else if (movement.z < 0)
                facing = (int)Direction.SouthWest;
            else
                facing = (int)Direction.South;
        }
        else // East-West
        {
            if (movement.z > 0)
                facing = (int)Direction.East;
            else if (movement.z < 0)
                facing = (int)Direction.West;
        }
    }

    void RotatePlayer()
    {
        Vector3 playerRot = Vector3.zero;

        switch (facing)
        {
            case (int)Direction.North:
=======
    void RotatePlayer() {
        //Quaternion playerRot = transform.rotation;
        Vector3 playerRot = new Vector3(0,0,0);
        //Debug.Log("Facing Read: " + facing);
        switch (facing) {
            case 0: //North
>>>>>>> parent of 1fdb671 (Revert "Merge branch 'main' into jacob-enemy")
                playerRot.z = 1f;
                break;
            case (int)Direction.NorthEast:
                playerRot.x = -1f;
                playerRot.z = 1f;
                break;
            case (int)Direction.East:
                playerRot.x = -1f;
                break;
            case (int)Direction.SouthEast:
                playerRot.x = -1f;
                playerRot.z = -1f;
                break;
            case (int)Direction.South:
                playerRot.z = -1f;
                break;
            case (int)Direction.SouthWest:
                playerRot.x = 1f;
                playerRot.z = -1f;
                break;
            case (int)Direction.West:
                playerRot.x = 1f;
                break;
            case (int)Direction.NorthWest:
                playerRot.x = 1f;
                playerRot.z = 1f;
                break;
        }

        transform.LookAt(transform.position + playerRot);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}