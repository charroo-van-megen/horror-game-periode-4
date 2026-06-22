using TMPro;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    public GameObject player;
    public GameObject KeypadOB;
    public GameObject hud;
    public GameObject inv;

    public GameObject animateOB;
    public Animator ANI;

    public TMP_Text textOB; // TextMeshPro

    public string anwser = "1234";

    public AudioSource source;
    public AudioClip button;
    public AudioClip correct;
    public AudioClip wrong;

    public bool animate;

    void Start()
    {
        textOB.text = "";
    }


    void ResetCode()
    {
        textOB.text = "";
    }

    void checkcode()
    {
        if (textOB.text == anwser)
        {
            textOB.text = "Right";

            if (source != null && correct != null)
                source.PlayOneShot(correct);

            if (animate && ANI != null)
            {
                ANI.SetBool("animate", true);
                Debug.Log("Door Open");
            }
        }
        else
        {
            textOB.text = "wrong : (";

            if (source != null && wrong != null)
                source.PlayOneShot(wrong);

            Invoke(nameof(ResetCode), 1f);
        }
    }

    public void Number(int number)
    {
        if (textOB == null) return;

        // Niet meer dan 4 cijfers
        if (textOB.text.Length >= 4)
            return;

        textOB.text += number.ToString();

        if (source != null && button != null)
            source.PlayOneShot(button);

        // Controleer zodra 4 cijfers zijn ingevoerd
        if (textOB.text.Length == 4)
        {
            checkcode();
        }
    }

    public void Exit()
    {
        if (KeypadOB != null)
            KeypadOB.SetActive(false);

        if (inv != null)
            inv.SetActive(true);

        if (hud != null)
            hud.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (textOB != null && textOB.text == anwser)
        {
            textOB.text = "Right";

            if (animate && ANI != null)
            {
                ANI.SetBool("animate", true);
                Debug.Log("Its Open");
            }
        }


 
    }
}