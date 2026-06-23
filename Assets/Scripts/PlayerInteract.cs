using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera playerCam;

    private void Start()
    {
        if (playerCam == null)
        {
            playerCam = GetComponent<Camera>();
        }
        if (playerCam == null)
        {
            playerCam = GetComponentInChildren<Camera>();
        }

        if (playerCam == null)
        {
            Debug.LogError("PlayerInteract: No Camera found on Player or its children!");
            return;
        }
    }

    void Update()
    {
        if (playerCam == null)
            return;

        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        RaycastHit hit;

        // On pressing E, interact
        bool eKeyPressed = false;

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            eKeyPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            eKeyPressed = true;
        }

        if (eKeyPressed)
        {
            Debug.Log("E key pressed, checking for interactable objects...");
            if (Physics.Raycast(ray, out hit, 3f))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    Debug.Log("Interacting with " + hit.collider.name);
                    interactable.Interact();
                }
            }
        }
    }
}
