using UnityEngine;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody rb;
    private VisualEffect atc;
    private Vector3 movement;
    private bool isGrounded;
    
    private bool jumpInput;
    private bool attackInput;
    private bool canAttack = true;

    private enum Direction 
    {
        North,
        East,
        South,
        West
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
    }

    void FixedUpdate()
    {
        MovePlayer();
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

    void Attack() {
        atc.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}