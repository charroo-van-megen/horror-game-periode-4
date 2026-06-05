using UnityEngine;

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
        // Draw a ray forward from the camera, if anything is hit, check if it has an Interactable component and call Interact() on it
        if (playerCam != null)
        {
            Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 3f))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}
