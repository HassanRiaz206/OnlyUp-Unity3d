using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 100f;
    public float jumpForce = 10f;
    public float climbingJumpForce = 15f;
    public float climbDistance = 1.5f;
    public float climbSpeed = 2f;
    private bool isNearClimbable = false;

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded = true;
    private bool isClimbing = false;
    private bool hasJumped = false; // New flag to track jumping
    private Vector3 originalGravity;
    private float originalDrag;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        originalGravity = rb.useGravity ? rb.mass * Physics.gravity : Vector3.zero;

        originalDrag = rb.drag;
    }

    private void Update()
    {
        // Player movement and rotation
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (!isClimbing)
        {
            Vector3 movement = transform.forward * verticalInput * movementSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);
            rb.MovePosition(rb.position + movement);

            // Update animation parameters
            animator.SetBool("Run", verticalInput != 0f);

            // Jumping
            if (Input.GetKeyDown(KeyCode.Space) && !hasJumped && (isGrounded || isNearClimbable))
            {
                Jump();
            }

            // Climb input
            if (Input.GetKeyDown(KeyCode.F) && isNearClimbable && !isClimbing)
            {
                isClimbing = true;
                StartClimbing();
            }
        }
        else
        {
            // Climb controls and exit condition
            float climbInput = verticalInput;
            Vector3 climbVelocity = transform.up * climbInput * climbSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + climbVelocity);

            // Stop climbing if "F" key is pressed again or if the player is no longer near a climbable surface
            if (Input.GetKeyDown(KeyCode.F) || !isNearClimbable)
            {
                isClimbing = false;
                StopClimbing();
                // Reset animation states when stopping climbing
                animator.SetBool("Jump", false);
                animator.SetBool("Climb", false);
            }
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        hasJumped = true;
        animator.SetBool("Jump", true);
    }


    private void StartClimbing()
    {
        rb.useGravity = false;
        rb.drag = 10f;
        animator.SetBool("Climb", true);
    }

    private void StopClimbing()
    {
        rb.useGravity = true;
        rb.drag = originalDrag;
        animator.SetBool("Climb", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            hasJumped = false; // Reset jump flag when grounded
            animator.SetBool("Jump", false);
        }
    }

    private void FixedUpdate()
    {
        // Climbable detection using raycast
        isNearClimbable = Physics.Raycast(transform.position, transform.forward, climbDistance);
    }
}
