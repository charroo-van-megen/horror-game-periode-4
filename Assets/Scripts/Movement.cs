using System;
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

    // Event fired whenever the player jumps
    public Action OnJump;

    // =========================
    // PUBLIC READ-ONLY ACCESS
    // =========================

    public bool IsGrounded => isGrounded;
    public bool IsCrouching => isCrouching;
    public float CurrentSpeed => currentSpeed;
    public float MoveInput => moveInput;
    public float RotationInput => rotationInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        currentSpeed = walkSpeed;
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");

        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundLayer
        );

        HandleSprint();
        HandleCrouch();

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded && !isCrouching)
            {
                Jump();
            }
        }
    }

    private void FixedUpdate()
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

        // Notify listeners (such as FootstepAudio)
        OnJump?.Invoke();
    }

    private void HandleSprint()
    {
        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift) && moveInput > 0)
            currentSpeed = sprintSpeed;
        else
            currentSpeed = walkSpeed;
    }

    private void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
            capsule.height = crouchHeight;
            currentSpeed = crouchSpeed;
        }
        else
        {
            isCrouching = false;
            capsule.height = standingHeight;
        }
    }
}