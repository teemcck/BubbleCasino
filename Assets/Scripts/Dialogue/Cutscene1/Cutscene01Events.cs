using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene01Events : MonoBehaviour
{
    //References
    public GameObject fadeScreenIn;
    public GameObject textBox;

    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextObject;
    [SerializeField] GameObject charName;

    [SerializeField] GameObject bobbyRed;
    public Animator bobbyAnimator;
    public AudioSource bobbyAudioSource;

    private bool isTalking = false;



    //Next Button
    [SerializeField] GameObject nextButton;
    [SerializeField] int eventPos = 0;
    [SerializeField] GameObject fadeScreenOut;

    //Skip Feature
    private bool skipText = false;
    private bool textRunning = false;

    //SoundControl
    [SerializeField] AudioSource audioSource;
    private IEnumerator FadeOutMusic(float fadeDuration)
{
    float startVolume = audioSource.volume;

    while (audioSource.volume > 0)
    {
        audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
        yield return null;
    }

    audioSource.Stop();
    audioSource.volume = startVolume; // Reset volume for next use
}

    void Update() {

        textLength = TextCreator.charCount;

        // Check for spacebar input to skip text
        if (Input.GetKeyDown(KeyCode.Space))
        {
            skipText = true;
        }

    }
    IEnumerator DisplayText()
    {
        skipText = false;
        textRunning = true;
        isTalking = true;

        // Start talking animation
        bobbyAnimator.SetTrigger("Talk");

        // Start Bobby's talking animation and audio
        bobbyAnimator.Play("BobbyTalk");
        if (bobbyAudioSource != null && !bobbyAudioSource.isPlaying)
        {
            bobbyAudioSource.Play(); // Start the looping audio
        }

        // Clear text and start animation
        textBox.GetComponent<TMPro.TMP_Text>().text = "";
        for (int i = 0; i < textToSpeak.Length; i++)
        {
            if (skipText)
            {
                // Immediately display the full text and break the loop
                textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
                break;
            }

            // Add one character at a time
            textBox.GetComponent<TMPro.TMP_Text>().text += textToSpeak[i];
            yield return new WaitForSeconds(0.05f); // Adjust typing speed here
        }

        // Ensure the full text is displayed if skipping happens
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;

        // Stop talking animation and switch back to idle
        bobbyAnimator.SetTrigger("Idle");

        // Stop Bobby's talking animation and audio
        isTalking = false;
        bobbyAnimator.Play("BobbyIdle");
        if (bobbyAudioSource != null && bobbyAudioSource.isPlaying)
        {
            bobbyAudioSource.Stop(); // Stop the looping audio
        }

        textRunning = false; // Mark text as finished
    }

    void Start()
    {
        bobbyAnimator = bobbyRed.GetComponent<Animator>();
        StartCoroutine(EventStart());
    }

    IEnumerator EventStart()
    {
        // Event 0
        yield return new WaitForSeconds(2);
        textBox.SetActive(true);

        charName.GetComponent<TMPro.TMP_Text>().text = "Bobby Red";

        // Text here
        mainTextObject.SetActive(true);
        textToSpeak = "Welcome to Bubble casino!";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        
        eventPos = 1;
    }

    IEnumerator EventOne()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        textToSpeak = "Where Your treasure is our pleasure!";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 2;
    }

    IEnumerator Event02()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        textToSpeak = "Drown yourself into our wide selection of games where you and your lovely children can bet big!";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 3;
    }

    IEnumerator Event03()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        textToSpeak = "I can tell you guys are excited to play!";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 4;
    }

    IEnumerator Event04()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        //cut to frame of family up close
        charName.GetComponent<TMPro.TMP_Text>().text = "You";

        textToSpeak = "...";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 5;
    }

    IEnumerator Event05()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        //cut to frame of family up close
        charName.GetComponent<TMPro.TMP_Text>().text = "Bobby Red";

        textToSpeak = "Well what are you waiting for? Get your asses to those slots and start spending money!";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 6;
    }

    IEnumerator Event06()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);
        fadeScreenOut.SetActive(true);
        yield return new WaitForSeconds(2);

   
        SceneManager.LoadScene(2);
    }

    public void NextButton()
    {
        if (eventPos == 1)
        {
            StartCoroutine(EventOne());
        }

        if (eventPos == 2)
        {
            StartCoroutine(Event02());
        }

        if (eventPos == 3)
        {
            StartCoroutine(Event03());
        }

        if (eventPos == 4)
        {
            StartCoroutine(Event04());
        }

        if (eventPos == 5)
        {
            StartCoroutine(Event05());
        }
        if (eventPos == 6)
        {
            StartCoroutine(Event06());
        }
    }
}
