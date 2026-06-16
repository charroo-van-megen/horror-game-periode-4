using Unity.VisualScripting;
using UnityEngine;

public class LightController : MonoBehaviour, Interactable
{
    private bool status = false;
    private Light light;

    void Start()
    {
        light = GetComponent<Light>();
    }

    public void Interact()
    {
        status = !status;
        light.enabled = status;
    }
}