using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float groundDrag = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpCooldown = 0.25f;
    [SerializeField] private float airMultiplier = 0.4f;
    
    [Header("Ground Check")]
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;
    
    [Header("Camera Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float maxLookAngle = 90f;
    
    private Rigidbody rb;
    private Vector3 moveDirection;
    private float xRotation = 0f;
    private bool canJump = true;
    
    // New Input System
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction escapeAction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        if (rb == null)
        {
            Debug.LogError("PlayerMove: No Rigidbody found! Please add a Rigidbody component to the player.");
            enabled = false;
            return;
        }
        
        // Configure Rigidbody
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        
        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();
        
        if (playerCamera == null)
            playerCamera = Camera.main;
        
        if (playerCamera == null)
            Debug.LogError("PlayerMove: No camera found! Please assign a camera or ensure one exists as a child object.");
        
        SetupInputActions();
        
        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        
        Debug.Log("PlayerMove initialized successfully!");
    }

    private void SetupInputActions()
    {
        var keyboard = Keyboard.current;
        var mouse = Mouse.current;
        
        if (keyboard == null || mouse == null)
        {
            Debug.LogError("PlayerMove: Keyboard or mouse not found!");
            return;
        }
        
        // Movement: WASD
        moveAction = new InputAction(type: InputActionType.Value, binding: "<Keyboard>/w,<Keyboard>/a,<Keyboard>/s,<Keyboard>/d");
        moveAction.AddBinding("<Keyboard>/upArrow");
        moveAction.AddBinding("<Keyboard>/leftArrow");
        moveAction.AddBinding("<Keyboard>/downArrow");
        moveAction.AddBinding("<Keyboard>/rightArrow");
        
        // Look: Mouse Delta
        lookAction = new InputAction(type: InputActionType.Value, binding: "<Mouse>/delta");
        
        // Jump: Space
        jumpAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/space");
        
        // Sprint: Left Shift
        sprintAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/leftShift");
        
        // Escape: ESC
        escapeAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/escape");
        
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
        escapeAction.Enable();
    }

    private void Update()
    {
        // Ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        
        HandleInput();
        if (playerCamera != null)
            HandleCamera();
        ControlSpeed();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleInput()
    {
        // Movement input using WASD
        if (Keyboard.current != null)
        {
            float horizontalInput = 0f;
            float verticalInput = 0f;
            
            if (Keyboard.current.wKey.isPressed) verticalInput += 1f;
            if (Keyboard.current.sKey.isPressed) verticalInput -= 1f;
            if (Keyboard.current.dKey.isPressed) horizontalInput += 1f;
            if (Keyboard.current.aKey.isPressed) horizontalInput -= 1f;
            
            moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
            
            // Jump input
            if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded && canJump)
            {
                Jump();
            }
            
            // Unlock cursor
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    private void HandleCamera()
    {
        if (Mouse.current == null)
            return;
        
        // Get mouse delta
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        
        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;
        
        // Rotate player body left/right
        transform.Rotate(Vector3.up * mouseX);
        
        // Rotate camera up/down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);
        
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void ControlSpeed()
    {
        bool isSprinting = Keyboard.current != null && Keyboard.current.leftShiftKey.isPressed;
        float targetSpeed = isSprinting && isGrounded ? sprintSpeed : moveSpeed;
        
        if (moveDirection.magnitude > 0)
        {
            rb.linearVelocity = new Vector3(moveDirection.normalized.x * targetSpeed, rb.linearVelocity.y, moveDirection.normalized.z * targetSpeed);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void MovePlayer()
    {
        if (moveDirection.magnitude > 0)
        {
            bool isSprinting = Keyboard.current != null && Keyboard.current.leftShiftKey.isPressed;
            float speed = isSprinting ? sprintSpeed : moveSpeed;
            float multiplier = isGrounded ? 1f : airMultiplier;
            rb.AddForce(moveDirection.normalized * speed * multiplier, ForceMode.Acceleration);
        }
        
        // Apply drag
        rb.linearDamping = isGrounded ? groundDrag : 0;
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        
        canJump = false;
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void ResetJump()
    {
        canJump = true;
    }

    private void OnDestroy()
    {
        moveAction?.Dispose();
        lookAction?.Dispose();
        jumpAction?.Dispose();
        sprintAction?.Dispose();
        escapeAction?.Dispose();
    }
}
