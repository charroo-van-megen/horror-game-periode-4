using UnityEngine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    Camera playerCam;

    private void Start()
    {
        playerCam = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        // On pressing E, we will check if there is an interactable object in front of the player using a RayCast, then execute the Interact() method on that object.
        if (playerCam != null && Keyboard.current != null)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                Debug.Log("E key pressed, checking for interactable objects...");
                Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
                RaycastHit hit;
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
}
