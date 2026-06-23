using UnityEngine;



public class FirstPersonController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float mouseSensitivity = 200f;

    public Transform playerCamera;

    float xRotation = 0f;
    bool cursorUnlocked = false;

    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        // E om cursor aan/uit te zetten
        if (Input.GetKeyDown(KeyCode.E))
        {
            cursorUnlocked = !cursorUnlocked;

            if (cursorUnlocked)
                UnlockCursor();
            else
                LockCursor();
        }

        // Alleen kijken als cursor vergrendeld is
        if (!cursorUnlocked)
        {
            Look();
        }

        Move();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement =
            transform.right * horizontal +
            transform.forward * vertical;

        transform.position += movement * movementSpeed * Time.deltaTime;
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}