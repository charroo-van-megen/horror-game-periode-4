using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashlightLight;
    private bool isOn = false;

    private void Start()
    {
        flashlightLight.enabled = false;
    }

    public void Toggle()
    {
        isOn = !isOn;
        flashlightLight.enabled = isOn;
    }
}