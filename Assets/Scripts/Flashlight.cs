using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Light flashlight;
    bool isOn = false;

    float innerMinAngle = 0;
    float innerMaxAngle = 40;
    float outerMinAngle = 50;
    float outerMaxAngle = 90;
    
    float scrollSensitivity = 2f;

    private void Start()
    {
        // Try to get Light from this GameObject first
        if (flashlight == null)
            flashlight = GetComponent<Light>();
        
        // If not found, search in children
        if (flashlight == null)
            flashlight = GetComponentInChildren<Light>();
        
        if (flashlight == null)
            Debug.LogError("Flashlight: No Light component found on this GameObject or its children!");
    }

    void Update()
    {
        if (flashlight == null)
            return;

        // Toggle flashlight with RMB
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }

        // Scroll up or down to adjust both inner and outer angles proportionally
        if (Mouse.current != null)
        {
            float scrollInput = Mouse.current.scroll.y.ReadValue();

            if (scrollInput > 0f) // scroll up - increase angles
            {
                flashlight.innerSpotAngle = Mathf.Clamp(flashlight.innerSpotAngle + scrollSensitivity, innerMinAngle, innerMaxAngle);
                flashlight.spotAngle = Mathf.Clamp(flashlight.spotAngle + scrollSensitivity, outerMinAngle, outerMaxAngle);
            }
            else if (scrollInput < 0f) // scroll down - decrease angles
            {
                flashlight.innerSpotAngle = Mathf.Clamp(flashlight.innerSpotAngle - scrollSensitivity, innerMinAngle, innerMaxAngle);
                flashlight.spotAngle = Mathf.Clamp(flashlight.spotAngle - scrollSensitivity, outerMinAngle, outerMaxAngle);
            }
        }
    }
}
