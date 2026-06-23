using System.Collections;
using Unity.Multiplayer.Center.Common.Analytics;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    [SerializeField] private bool isOpen = false;
    private float rotationSpeed = 90f; // degrees per second

    public void Interact()
    {
        if (!isOpen)
        {
            StartCoroutine(OpenDoor());
        }
        else
        {
            StartCoroutine(CloseDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        float elapsedTime = 0f;
        float targetRotation = 1f; // normalized rotation value for -90 degrees

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * rotationSpeed / 90f;
            gameObject.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(0, -90, elapsedTime), 0);
            yield return null;
        }

        gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
        isOpen = true;
    }

    IEnumerator CloseDoor()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * rotationSpeed / 90f;
            gameObject.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(-90, 0, elapsedTime), 0);
            yield return null;
        }

        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        isOpen = false;
    }
}
