using UnityEngine;

public class Button : MonoBehaviour, Interactable
{
    public void Interact()
    {
        Debug.Log("Button was pressed!");
    }
}
