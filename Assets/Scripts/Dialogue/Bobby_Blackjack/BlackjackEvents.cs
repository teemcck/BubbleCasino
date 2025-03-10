using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackjackEvents : MonoBehaviour
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
    [SerializeField] GameObject bobbyRedLeaves;
    public Animator bobbyAnimator;
    public AudioSource bobbyAudioSource;

    [SerializeField] GameObject fryGuy;
    [SerializeField] GameObject fryToken;
    [SerializeField] GameObject mommyToken;
    public Animator fryAnimator;
    public AudioSource fryAudioSource;

    [SerializeField] GameObject placeBets;

    private bool isTalking = false;
    public bool dialogFinished = false;

    //Next Button
    [SerializeField] GameObject nextButton;
    [SerializeField] int eventPos = 0;
    [SerializeField] GameObject fadeScreenOut;

    [SerializeField] GameObject quitConfirmationBox;

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

        // Start Fry's talking audio (looping)
        if (fryAudioSource != null && !fryAudioSource.isPlaying)
        {
            fryAudioSource.loop = true; // Ensure the audio loops
            fryAudioSource.Play(); // Start the looping audio
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

        // Stop Fry's talking audio
        isTalking = false;
        if (fryAudioSource != null && fryAudioSource.isPlaying)
        {
            fryAudioSource.Stop(); // Stop the looping audio
        }

        textRunning = false; // Mark text as finished
    }

    void Start()
    {
        bobbyAnimator = bobbyRed.GetComponent<Animator>();
    }

    public IEnumerator EventStart()
    {
        // Ensure this flag is reset
        dialogFinished = false;

        // Activate text box and character
        textBox.SetActive(true);
        bobbyRed.SetActive(true);

        // Set the character's name
        charName.GetComponent<TMPro.TMP_Text>().text = "Bobby Red";

        // Set the text to be displayed
        mainTextObject.SetActive(true);
        textToSpeak = "Ah, good ol' blackjack!";
        currentTextLength = textToSpeak.Length;

        // Wait for the dialog coroutine to complete
        yield return StartCoroutine(DisplayText());

        // Wait a moment before enabling the next button
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);

        // Update event position
        eventPos = 1;
    }

    IEnumerator Event02()
    {
        textBox.SetActive(true);
        bobbyRed.SetActive(true);
        nextButton.SetActive(false);

        charName.GetComponent<TMPro.TMP_Text>().text = "Bobby Red";

        // Text here
        mainTextObject.SetActive(true);
        textToSpeak = "Y'know I once played blackjack with a hunky lookin' shark.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        
        eventPos = 2;
    }

    IEnumerator Event03()
    {
        textBox.SetActive(true);
        bobbyRed.SetActive(true);
        nextButton.SetActive(false);

        charName.GetComponent<TMPro.TMP_Text>().text = "Bobby Red";

        // Text here
        mainTextObject.SetActive(true);
        textToSpeak = "Won every round. The poor guy couldn’t bluff to save his life.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        
        eventPos = 3;
    }

    IEnumerator Event04()
    {
        textBox.SetActive(true);
        bobbyRed.SetActive(true);
        nextButton.SetActive(false);

        charName.GetComponent<TMPro.TMP_Text>().text = "Bobby Red";

        // Text here
        mainTextObject.SetActive(true);
        textToSpeak = "The rules are simple: get as close to 21 as you can without going over. Hit, stand, double down, split—it’s like pickin’ bubbles in the tide! Easy, right?";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        
        eventPos = 4;
    }

    IEnumerator Event05()
    {
        textBox.SetActive(true);
        bobbyRed.SetActive(true);
        nextButton.SetActive(false);

        charName.GetComponent<TMPro.TMP_Text>().text = "Bobby Red";

        // Text here
        mainTextObject.SetActive(true);
        textToSpeak = "Here. I'll leave it to my pal, Fry. He'll be your server.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        
        eventPos = 5;
    }


    IEnumerator Event06()
    {
        textBox.SetActive(true);
        bobbyRed.SetActive(true);
        nextButton.SetActive(false);

        charName.GetComponent<TMPro.TMP_Text>().text = "Bobby Red";

        // Text here
        mainTextObject.SetActive(true);
        textToSpeak = "Well, good luck suckers!";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        
        eventPos = 6;
    }

    IEnumerator Event07()
    {
        textBox.SetActive(true);
        nextButton.SetActive(false);
        

        charName.GetComponent<TMPro.TMP_Text>().text = "Bobby Red";

        bobbyRed.SetActive(false);
        bobbyRedLeaves.SetActive(true);

        fryGuy.SetActive(true);
        
        charName.GetComponent<TMPro.TMP_Text>().text = "Fry";

        // Text here
        mainTextObject.SetActive(true);
        textToSpeak = "Greetings, Mademoiselle.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        
        eventPos = 7;
    }

    IEnumerator Event08()
    {
        textBox.SetActive(true);
        nextButton.SetActive(false);
        bobbyRedLeaves.SetActive(false);
        

        charName.GetComponent<TMPro.TMP_Text>().text = "Fry";

        // Text here
        mainTextObject.SetActive(true);
        textToSpeak = "Are you ready to place your bets?";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        
        eventPos = 8;
    }

    IEnumerator Event09()
    {
        textBox.SetActive(false);
        mainTextObject.SetActive(false);
        nextButton.SetActive(false);
        

        yield return new WaitForSeconds(0.05f);

        placeBets.SetActive(true);
        
        eventPos = 9;

        // Signal completion
        dialogFinished = true;
        fryGuy.SetActive(false);
        fryToken.SetActive(true);
        mommyToken.SetActive(true);
    }

    public void NextButton()
    {
        if (eventPos == 1)
        {
            StartCoroutine(Event02());
        }

        if (eventPos == 2)
        {
            StartCoroutine(Event03());
        }

        if (eventPos == 3)
        {
            StartCoroutine(Event04());
        }

        if (eventPos == 4)
        {
            StartCoroutine(Event05());
        }

        if (eventPos == 5)
        {
            StartCoroutine(Event06());
        }
        if (eventPos == 6)
        {
            StartCoroutine(Event07());
        }
        if (eventPos == 7)
        {
            StartCoroutine(Event08());
        }
        if (eventPos == 8)
        {
            StartCoroutine(Event09());
        }
    }

     public void QuitBlackJackGame()
    {   
        StartCoroutine(QuitBlackjack()); 
    }

    IEnumerator QuitBlackjack()
    {   
        quitConfirmationBox.SetActive(false);
        fadeScreenOut.SetActive(true);

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    } 
}
