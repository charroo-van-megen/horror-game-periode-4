using UnityEngine;

public class Locker : MonoBehaviour
{
    [Header("References")]
    public Transform hidePosition;
    public Transform exitPosition;

    public GameObject player;
    public MonoBehaviour playerMovement;

    [Header("Player Visuals")]
    public GameObject playerModel; 
    // Drag the visual model/body here

    [Header("Settings")]
    public KeyCode interactKey = KeyCode.E;
    public float interactDistance = 2f;

    private bool isPlayerNearby;
    private bool isHiding;

    private void Update()
    {
        CheckDistance();

        if (isPlayerNearby && Input.GetKeyDown(interactKey))
        {
            if (!isHiding)
                EnterLocker();
            else
                ExitLocker();
        }
    }

    private void CheckDistance()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(player.transform.position, transform.position);
        isPlayerNearby = distance <= interactDistance;
    }

    private void EnterLocker()
    {
        isHiding = true;

        player.transform.position = hidePosition.position;

        if (playerMovement != null)
            playerMovement.enabled = false;

        // Hide player model
        if (playerModel != null)
            playerModel.SetActive(false);

        Debug.Log("Entered Locker");
    }

    private void ExitLocker()
    {
        isHiding = false;

        player.transform.position = exitPosition.position;

        if (playerMovement != null)
            playerMovement.enabled = true;

        // Show player model again
        if (playerModel != null)
            playerModel.SetActive(true);

        Debug.Log("Exited Locker");
    }
}