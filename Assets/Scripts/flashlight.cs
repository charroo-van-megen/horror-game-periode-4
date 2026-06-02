using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashlightLight;
    private bool isOn = false;

    private void Start()
    {
        flashlightLight.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        isOn = !isOn;
        flashlightLight.enabled = isOn;
        Debug.Log("Flashlight " + (isOn ? "On" : "Off"));
    }
}