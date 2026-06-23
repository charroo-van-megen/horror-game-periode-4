using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Movement3D movement;

    [Header("Audio")]
    [SerializeField] private AudioClip footstepLoop;
    [SerializeField] private AudioClip jumpClip;

    [SerializeField] private float walkPitch = 1f;
    [SerializeField] private float sprintPitch = 1.25f;
    [SerializeField] private float crouchPitch = 0.8f;

    private AudioSource audioSource;

    private void OnEnable()
    {
        if (movement != null)
            movement.OnJump += PlayJumpSound;
    }

    private void OnDisable()
    {
        if (movement != null)
            movement.OnJump -= PlayJumpSound;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = footstepLoop;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        bool isMoving =
            Mathf.Abs(movement.MoveInput) > 0.1f &&
            movement.IsGrounded;

        if (isMoving)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();

            if (movement.IsCrouching)
                audioSource.pitch = crouchPitch;
            else if (movement.CurrentSpeed > 5f)
                audioSource.pitch = sprintPitch;
            else
                audioSource.pitch = walkPitch;
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }

    private void PlayJumpSound()
    {
        if (jumpClip != null)
            AudioSource.PlayClipAtPoint(jumpClip, transform.position);
    }
}