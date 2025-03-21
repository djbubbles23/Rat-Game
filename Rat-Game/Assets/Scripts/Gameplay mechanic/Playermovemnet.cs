
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;            //Movement speed
    public float jumpForce = 7f;            //Jump strength
    public GameObject attackHitbox;         //object that represents player attack hitbox
    private Rigidbody rb;                   //Rigidbody of the player gameobject
    private VisualEffect atc;               //VisualEffect component that creates the player's attacks
    private Vector3 movement;               //movement vector based on player inputs
    private bool isGrounded;                //state flag that the player is on the ground
    private bool idle;                      //state flag that the player is not moving
    private bool jumpInput;                 //state flag that the player has pressed the jump button
    
    private bool attackInput;               //state flag that the player has pressed the attack button
    private bool canAttack = true;          //state flag of weather or not the player can attack
    public float attackDelay = 1f;          //delay between attacks in seconds
    private float attackCounter;            //counted progress of the delay between attacks in seconds
    private int facing;                     //state holder for the direction the player is facing

    //Animation State Machine
    Animator playerAnim;

    public void Awake()
    {
        playerAnim = GetComponent<Animator>();
    }

    private enum Direction                  //Enum for the direction the player is facing
    {
        North, //Facing the +X direction (Right on the Camera)
        NorthEast,
        East, //Facing the +Z direction (Away From the Camera)
        SouthEast,
        South, //Facing the -X direction (Left on the Camera)
        SouthWest,
        West, //Facing the -Z direction (Towards the Camera)
        NorthWest
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        atc = gameObject.GetComponentInChildren<VisualEffect>();
    }

    void Update()
    {
        CheckInputs();

        if (jumpInput && isGrounded)
        {
            Jump();
        }
        if (attackInput && canAttack) {
            Attack();
        }
        else if (!canAttack) {
            attackCounter += Time.fixedDeltaTime;

            if (attackCounter >= attackDelay) {
                canAttack = true;
                attackCounter = 0;
            }
        } 

        Vector3 movementDirection = new Vector3(movement.x, 0, movement.z);
        float magniture =  Mathf.Clamp01(movementDirection.magnitude)*moveSpeed;
        movementDirection.Normalize();
        if(movementDirection.magnitude > 0)
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

    void CheckInputs() {
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
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void Attack()
    {

        canAttack = false;
        atc.Play();
        
        // handle attacking enemy
        StartCoroutine(ActivateAttackHb());

    }

    IEnumerator ActivateAttackHb()
    {
        Debug.Log("Attacking t");
        playerAnim.SetBool("Attack1", true);
        // activate attack hitbox for 0.1 seconds
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        attackHitbox.SetActive(false);
        Debug.Log("Attacking f");
        playerAnim.SetBool("Attack1", false);
    }

    void DirectionCheck() { //Direction State Machine
        if (movement.x == 0 && movement.z == 0) {
            idle = true;
        }
        else {
            idle = false;
            if (movement.x > 0) { //North
                if (movement.z > 0) {
                    facing = (int) Direction.NorthEast;
                }
                else if (movement.z < 0) {
                    facing = (int) Direction.NorthWest;
                }
                else
                {
                    facing = (int) Direction.North;
                }
            }
            else if (movement.x < 0) { //South
                if (movement.z > 0) {
                    facing = (int) Direction.SouthEast;
                }
                else if (movement.z < 0) {
                    facing = (int) Direction.SouthWest;
                }
                else {
                    facing = (int) Direction.South;
                }
            }
            else { //East-West
                if (movement.z > 0) {
                    facing = (int) Direction.East;
                }
                else if (movement.z < 0) {
                    facing = (int) Direction.West;
                }
                else {
                    idle = true;
                    Debug.Log("Fix Your Damn Code: Direction State Machine Reached Illegal Location");
                }
            }
        }
        
    }

    void RotatePlayer() {
        //Quaternion playerRot = transform.rotation;
        Vector3 playerRot = new Vector3(0,0,0);
        Debug.Log("Facing Read: " + facing);
        switch (facing) {
            case 0: //North
                playerRot.z = 1f;
                break;

            case 1:
                playerRot.x = -1f;
                playerRot.z = 1f;
                break;

            case 2:
                playerRot.x = -1f;
                break;

            case 3:
                playerRot.x = -1f;
                playerRot.z = -1f;
                break;

            case 4:
                playerRot.z = -1f;
                break;

            case 5:
                playerRot.x = 1f;
                playerRot.z = -1f;                
                break;

            case 6:
                playerRot.x = 1f;
                break;

            case 7:
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