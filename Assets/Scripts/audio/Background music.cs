using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance;

    private AudioSource audioSource;

    private void Awake()
    {
        // Singleton check
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}