using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("References")]
    public Transform playerBody;

    [Header("Settings")]
    public float mouseSensitivity = 100f;

    private float xRotation = 0f;

    void Start()
    {
        // Load sensitivity
        mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity", 100f);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
{
    if (Time.timeScale == 0f)
        return;

    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    playerBody.Rotate(Vector3.up * mouseX);
}

    public void SetSensitivity(float sensitivity)
    {
        mouseSensitivity = sensitivity;

        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        PlayerPrefs.Save();
    }
}