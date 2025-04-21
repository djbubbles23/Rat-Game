using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;            // Movement speed
    public float jumpForce = 7f;           // Jump strength
    public GameObject attackHitbox;        // Object that represents player attack hitbox
    private Rigidbody rb;                  // Rigidbody of the player gameobject
    public VisualEffect atc;              // VisualEffect component for player's attacks
    private Vector3 movement;              // Movement vector based on player inputs
    private bool isGrounded;               // State flag for whether the player is on the ground
    private bool jumpInput;                // State flag for jump input
    private bool attackInput;              // State flag for attack input
    private bool canAttack = true;         // State flag for whether the player can attack
    public float attackDelay = 3f;         // Delay between attacks in seconds
    private float attackCounter;           // Counter for attack delay
    private int facing;                    // Direction the player is facing

    public weaponController WeaponController;
    private bool eWeaponEquipped = false; // State flag for whether a weapon is equipped

    // Animation State Machine
    private Animator playerAnim;

    public RuntimeAnimatorController daggerAnim;
    public RuntimeAnimatorController swordAnim;
    public RuntimeAnimatorController longSwordAnim;


    public AudioClip swingSFX;
    private AudioSource audioSource;

    public INVManager invManager; // Reference to the inventory manager

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
        //atc = gameObject.GetComponentInChildren<VisualEffect>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Function to handle weapon equip. Weapon model, animation controller, etc.
        weaponScriptableObject weaponTemp = invManager.weaponSlot.GetComponent<INVSlot>().heldItem?.GetComponent<INVItem>().weapon;
        if (weaponTemp != null){
            eWeaponEquipped = true;
            if(weaponTemp.weaponObj.gameObject.name == "DaggerOBJ"){
                changeWeapon("dagger");
            }
            else if(weaponTemp.weaponObj.gameObject.name == "SwordOBJ"){
                changeWeapon("sword");
            }
            else if(weaponTemp.weaponObj.gameObject.name == "LongSwordOBJ"){
                changeWeapon("longSword");
            }
        }
        else{
            
            eWeaponEquipped = false;
        }

        CheckInputs();

        if (jumpInput && isGrounded)
        {
            Jump();
        }

        if (attackInput && canAttack && movement == Vector3.zero && invManager.menuActivated == false && eWeaponEquipped)
        {
            Attack();
            AudioClip clip = swingSFX;
            audioSource.PlayOneShot(clip);
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


        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            playerAnim.SetTrigger("isDancing"); 
        }

        HandleMovementAnimations();

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
        playerAnim.SetTrigger("isAttacking");
        StartCoroutine(ActivateAttackHb());
        canAttack = false;
    }

    IEnumerator ActivateAttackHb()
    {
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(0.2f); // Duration of the attack hitbox
        atc.Play();
        attackHitbox.SetActive(false);

        yield return new WaitForSeconds(0.5f); // Wait for the attack animation to finish
        playerAnim.ResetTrigger("isAttacking");
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

 void DirectionCheck() { //Direction State Machine
        if (movement.x == 0 && movement.z == 0) {
        }
        else {
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

            }
        }
        
    }

    void RotatePlayer() {
        //Quaternion playerRot = transform.rotation;
        Vector3 playerRot = new Vector3(0,0,0);
        //Debug.Log("Facing Read: " + facing);
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

        if (collision.gameObject.CompareTag("Dice"))
        {
            invManager.ItemPicked(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Weapon"))
        {
            invManager.ItemPicked(collision.gameObject);
        }
    }

    public void changeWeapon(string weaponType){
        if(weaponType == "dagger"){
            //hitbox
            attackHitbox.transform.localScale = new Vector3(0.13f, 1f, 0.09f);
            //animation controller
            playerAnim.runtimeAnimatorController = daggerAnim;
            //speed
            attackDelay = .5f;
        }
        if(weaponType == "sword"){
            attackHitbox.transform.localScale = new Vector3(0.22f, 1f, 0.15f);
            playerAnim.runtimeAnimatorController = swordAnim;
            attackDelay = 1f;
        }
        if(weaponType == "longSword"){
            attackHitbox.transform.localScale = new Vector3(1.64f, 1f, 1.64f);
            playerAnim.runtimeAnimatorController = longSwordAnim;
            attackDelay = 2f;
        }
    }
}