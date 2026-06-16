using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("UI")]
    public Slider sensitivitySlider;
    public Slider volumeSlider;

    [Header("References")]
    public MouseLook mouseLook;

    private void Start()
    {
        // Load saved settings
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", 100f);
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);

        // Set slider values
        sensitivitySlider.value = savedSensitivity;
        volumeSlider.value = savedVolume;

        // Apply settings
        mouseLook.SetSensitivity(savedSensitivity);
        AudioListener.volume = savedVolume;

        // Add listeners
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetSensitivity(float value)
    {
        mouseLook.SetSensitivity(value);
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;

        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }
}