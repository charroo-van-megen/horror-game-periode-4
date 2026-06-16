using Unity.VisualScripting;
using UnityEngine;

public class Switch : MonoBehaviour, Interactable
{
    public GameObject[] objectsToToggle;
    private bool isOn = false;

    public void Interact()
    {
        isOn = !isOn;
        ToggleObjects();
    }

    private void ToggleObjects()
    {
        if (objectsToToggle == null) return;

        foreach (GameObject obj in objectsToToggle)
        {
            if (obj != null)
            {
                obj.SetActive(isOn);
            }
        }
    }
}
