using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public Camera playerCamera;
    public Transform handPoint;
    public float pickupDistance = 3f;

    private GameObject heldObject;
    private Rigidbody heldRb;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                TryPickup();
            }
            else
            {
                DropObject();
            }
        }
    }

    void TryPickup()
    {
        RaycastHit hit;

        if (Physics.Raycast(
            playerCamera.transform.position,
            playerCamera.transform.forward,
            out hit,
            pickupDistance))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                heldObject = hit.collider.gameObject;

                heldRb = heldObject.GetComponent<Rigidbody>();

                if (heldRb != null)
                {
                    heldRb.isKinematic = true;
                }

                heldObject.transform.SetParent(handPoint);
                heldObject.transform.localPosition = Vector3.zero;
                heldObject.transform.localRotation = Quaternion.identity;

                Debug.Log("Picked up " + heldObject.name);
            }
        }
    }

    void DropObject()
    {
        heldObject.transform.SetParent(null);

        if (heldRb != null)
        {
            heldRb.isKinematic = false;

            heldRb.AddForce(
                playerCamera.transform.forward * 2f,
                ForceMode.Impulse);
        }

        Debug.Log("Dropped " + heldObject.name);

        heldObject = null;
        heldRb = null;
    }

    void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(
                playerCamera.transform.position,
                playerCamera.transform.position + playerCamera.transform.forward * pickupDistance);
        }
    }
}