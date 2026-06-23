using UnityEngine;

public class Keypadaudio : MonoBehaviour
{
    public AudioClip enter;
    public AudioClip erase;
    public AudioClip tik;
    private AudioSource source;

    public void Start()
    {
        source = Camera.main.GetComponent<AudioSource>();

    }
    public void Playclip(AudioClip clipToPlay)
    {
        //source.Play(clipToPlay);
    }
}
