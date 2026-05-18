using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 4f;

    [SerializeField] private float sprintSpeed = 7f;

    [SerializeField] private float crouchSpeed = 2f;

    [SerializeField] private float rotationSpeed = 150f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 7f;

    [Header("Crouch")]
    [SerializeField] private float crouchHeight = 0.5f;

    [SerializeField] private float standingHeight = 2f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;

    [SerializeField] private float groundDistance = 0.4f;

    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rb;

    private CapsuleCollider capsule;

    private float moveInput;

    private float rotationInput;

    private bool isGrounded;

    private bool isCrouching;

    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        capsule = GetComponent<CapsuleCollider>();

        currentSpeed = walkSpeed;

        Debug.Log("Movement3D script started.");

        if (rb == null)
        {
            Debug.LogError("No Rigidbody attached!");
        }
        else
        {
            Debug.Log("Rigidbody found.");
        }

        if (capsule == null)
        {
            Debug.LogError("No CapsuleCollider attached!");
        }
        else
        {
            Debug.Log("CapsuleCollider found.");
        }
    }

    void Update()
    {
        // Input
        moveInput = Input.GetAxis("Vertical");

        rotationInput = Input.GetAxis("Horizontal");

        // Debug movement input
        if (moveInput != 0)
        {
            Debug.Log("Move Input: " + moveInput);
        }

        if (rotationInput != 0)
        {
            Debug.Log("Rotation Input: " + rotationInput);
        }

        // Ground Check
        bool wasGrounded = isGrounded;

        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundLayer
        );

        // Only log when ground state changes
        if (isGrounded != wasGrounded)
        {
            Debug.Log("Grounded: " + isGrounded);
        }

        // Sprint
        HandleSprint();

        // Crouch
        HandleCrouch();

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump button pressed.");

            if (isGrounded && !isCrouching)
            {
                Jump();

                Debug.Log("Jump executed.");
            }
            else
            {
                Debug.Log("Cannot jump.");
            }
        }
    }

    void FixedUpdate()
    {
        Move();

        Rotate();
    }

    private void Move()
    {
        Vector3 moveDirection = transform.forward * moveInput;

        Vector3 velocity = moveDirection * currentSpeed;

        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
    }

    private void Rotate()
    {
        float rotation = rotationInput * rotationSpeed * Time.fixedDeltaTime;

        transform.Rotate(0f, rotation, 0f);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x,
            jumpForce,
            rb.linearVelocity.z
        );
    }

    private void HandleSprint()
    {
        if (isCrouching)
        {
            currentSpeed = crouchSpeed;

            return;
        }

        if (Input.GetKey(KeyCode.LeftShift) && moveInput > 0)
        {
            currentSpeed = sprintSpeed;

            Debug.Log("Sprinting");
        }
        else
        {
            currentSpeed = walkSpeed;
        }
    }

    private void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (!isCrouching)
            {
                Debug.Log("Crouch started");
            }

            isCrouching = true;

            capsule.height = crouchHeight;

            currentSpeed = crouchSpeed;
        }
        else
        {
            if (isCrouching)
            {
                Debug.Log("Crouch ended");
            }

            isCrouching = false;

            capsule.height = standingHeight;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundDistance
        );
    }
}