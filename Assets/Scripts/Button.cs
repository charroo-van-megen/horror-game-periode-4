using System;
using Unity.VisualScripting;
using UnityEngine;



public class Button : MonoBehaviour, Interactable
{
    [SerializeField] private Interactable targetObj;

    public void Interact()
    {
        if (targetObj != null)
        {
            targetObj.Interact();
        }
    }
}
