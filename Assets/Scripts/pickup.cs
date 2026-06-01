using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform handPoint;
    public float pickupDistance = 3f;

    private Camera playerCamera;
    private Flashlight heldFlashlight;
    private Rigidbody heldRb;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldFlashlight == null)
                TryPickup();
            else
                Drop();
        }

        if (heldFlashlight != null && Input.GetMouseButtonDown(0))
        {
            heldFlashlight.Toggle();
        }
    }

    void TryPickup()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, pickupDistance))
        {
            Flashlight flashlight = hit.collider.GetComponent<Flashlight>();

            if (flashlight != null)
            {
                heldFlashlight = flashlight;
                heldRb = flashlight.GetComponent<Rigidbody>();

                heldRb.isKinematic = true;

                flashlight.transform.SetParent(handPoint);
                flashlight.transform.localPosition = Vector3.zero;
                flashlight.transform.localRotation = Quaternion.identity;
            }
        }
    }

    void Drop()
    {
        heldFlashlight.transform.SetParent(null);

        heldRb.isKinematic = false;

        heldRb.AddForce(
            playerCamera.transform.forward * 2f,
            ForceMode.Impulse);

        heldFlashlight = null;
        heldRb = null;
    }
}