using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject[] inventory = new GameObject[3];
    [SerializeField] private GameObject holdingItem;
    [SerializeField] private Camera playerCam;
    [SerializeField] private float pickupRange = 3f;

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
            Debug.LogError("Inventory: No Camera found!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Pickup();
        }

        foreach (var item in inventory)
        {
            if (item != null)
            {
                Debug.Log("Inventory item: " + item.name);
            }
        }
    }

    void Pickup()
    {
        if (playerCam == null)
            return;

        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Item"))
            {
                GameObject item = hit.collider.gameObject;
                AddToInventory(item);
            }
        }
    }

    private void AddToInventory(GameObject item)
    {
        // Find first empty slot in inventory
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                item.SetActive(false);
                Debug.Log("Added " + item.name + " to inventory slot " + i);
                return;
            }
        }

        Debug.Log("Inventory is full!");
    }
}
