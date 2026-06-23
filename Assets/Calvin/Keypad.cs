using TMPro;
using UnityEngine;
using System.Collections;

public class Keypad : MonoBehaviour
{
    public GameObject player;
    public GameObject KeypadOB;
    public GameObject hud;
    public GameObject inv;
    public GameObject cube;

    public GameObject animateOB;
   

    public TMP_Text textOB;

    public string anwser = "1234";

    public AudioSource source;
    public AudioClip button;
    public AudioClip correct;
    public AudioClip wrong;
    public AudioClip destroySound;

    public bool animate;

    void Start()
    {
        textOB.text = "";
    }

    void ResetCode()
    {
        textOB.text = "";
    }

    IEnumerator CorrectCode()
    {
        textOB.text = "Right";

        if (source != null && correct != null)
            source.PlayOneShot(correct);

        // 2 seconden wachten
        yield return new WaitForSeconds(2f);

       
        if (source != null && destroySound != null)
            source.PlayOneShot(destroySound);

        
        yield return new WaitForSeconds(1f);

        // Cube verwijderen
        if (cube != null)
            Destroy(cube);

       

        // Keypad sluiten
        if (KeypadOB != null)
            KeypadOB.SetActive(false);

        // HUD terug
        if (hud != null)
            hud.SetActive(true);

        if (inv != null)
            inv.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Number(int number)
    {
        if (textOB == null)
            return;

        // Maximaal 4 cijfers
        if (textOB.text.Length >= 4)
            return;

        textOB.text += number.ToString();

        if (source != null && button != null)
            source.PlayOneShot(button);

        // Zodra er 4 cijfers zijn ingevoerd
        if (textOB.text.Length == 4)
        {
            if (textOB.text == anwser)
            {
                StartCoroutine(CorrectCode());
            }
            else
            {
                textOB.text = "Wrong";

                if (source != null && wrong != null)
                    source.PlayOneShot(wrong);

                Invoke(nameof(ResetCode), 1f);
            }
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
}