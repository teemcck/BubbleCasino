using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelectionEvents : MonoBehaviour
{
 public GameObject textBox;

    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextObject;
    [SerializeField] GameObject charName;

    [SerializeField] GameObject slotsButton;
    [SerializeField] GameObject rouletteButton;

    private string selectedItem;

     public void SelectSlots()
    {
        selectedItem = "Slots";
        PlayDialog();
    }

    public void SelectRoulette()
    {
        selectedItem = "Roulette";
        PlayDialog();
    }

    public void PlayDialog () {

        if (selectedItem == "Slots")
        {
            StartCoroutine(SlotSelect());
        }

        if (selectedItem == "Roulette")
        {
            StartCoroutine(Roulette01());
        }
    }
   


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
    IEnumerator DisplayText(string animationTrigger = "Talk")
{
    skipText = false;
    textRunning = true;
    isTalking = true;

    // Start specified animation
    bobbyAnimator.SetTrigger(animationTrigger);

    // Start Bobby's audio if not already playing
    if (bobbyAudioSource != null && !bobbyAudioSource.isPlaying)
    {
        bobbyAudioSource.Play(); // Start the looping audio
    }

    // Clear text and display one character at a time
    textBox.GetComponent<TMPro.TMP_Text>().text = "";
    for (int i = 0; i < textToSpeak.Length; i++)
    {
        if (skipText)
        {
            textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
            break;
        }

        textBox.GetComponent<TMPro.TMP_Text>().text += textToSpeak[i];
        yield return new WaitForSeconds(0.05f); // Typing speed
    }

    // Ensure the full text is displayed
    textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;

    // Reset to idle animation after dialog
    bobbyAnimator.SetTrigger("Idle");

    if (bobbyAudioSource != null && bobbyAudioSource.isPlaying)
    {
        bobbyAudioSource.Stop(); // Stop Bobby's audio
    }

    isTalking = false;
    textRunning = false;
}

    void Start()
    {
        bobbyAnimator = bobbyRed.GetComponent<Animator>();
    }

    public void PlayBlackjack() {
        SceneManager.LoadScene(3);
    }

    IEnumerator SlotSelect()
    {
        textBox.SetActive(true);
        bobbyRed.SetActive(true);

        charName.GetComponent<TMPro.TMP_Text>().text = "Bobby Red";

        // Text here
        mainTextObject.SetActive(true);
        textToSpeak = "Aw shoot...";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText("GoofedUp"));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        
        eventPos = 1;
    }

    IEnumerator Slots01()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        textToSpeak = "The slot machines are all busted up.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 2;
    }

    IEnumerator Slots02()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        textToSpeak = "Yeah, thanks to some bozo he decided to destroy all of our machines because he blew away his 401K.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText("Talk"));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 3;
    }

    IEnumerator Slots03()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        textToSpeak = "Try choosing a different game to play, partner.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 4;
    }

    IEnumerator BobDisappear()
    {
        nextButton.SetActive(false);
        textBox.SetActive(false);
        mainTextObject.SetActive(false);
        bobbyRed.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        
        eventPos = 4;
    }


    private bool isFirstChoice = true;


    IEnumerator Roulette01()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);
        bobbyRed.SetActive(true);
        mainTextObject.SetActive(true);

        //cut to frame of family up close
        charName.GetComponent<TMPro.TMP_Text>().text = "Bobby Red";

        if (isFirstChoice)
        {
            textToSpeak = "Roulette's gonna be closed for a while.";
        }
        else
        {
            textToSpeak = "Roulette's gonna be closed for a while too...";
        }
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);

        isFirstChoice = false;

        eventPos = 6;
    }

    IEnumerator Roulette02()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        //cut to frame of family up close
        charName.GetComponent<TMPro.TMP_Text>().text = "Bobby Red";

        textToSpeak = "The supervisor is on her smoke break and she takes forever doing that in the daycare.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 7;
    }

    IEnumerator Roulette03()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        //cut to frame of family up close
        charName.GetComponent<TMPro.TMP_Text>().text = "Bobby Red";

        textToSpeak = "I always hate it when she comes out smelling like a loaded diaper. Real stinky.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 8;
    }

    IEnumerator Roulette04()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        //cut to frame of family up close
        charName.GetComponent<TMPro.TMP_Text>().text = "Bobby Red";

        textToSpeak = "...";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText("GoofedUp"));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 9;
    }

    IEnumerator Roulette05()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        //cut to frame of family up close
        charName.GetComponent<TMPro.TMP_Text>().text = "Bobby Red";

        textToSpeak = "Oh sorry, did I say too much?";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText());
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 10;
    }

    IEnumerator Roulette06()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        //cut to frame of family up close
        charName.GetComponent<TMPro.TMP_Text>().text = "Bobby Red";

        textToSpeak = "Well whatever! You can't play roulette right now!";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText("Talk"));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 4;
    }


    public void NextButton()
    {
        if (eventPos == 1)
        {
            StartCoroutine(Slots01());
        }

        if (eventPos == 2)
        {
            StartCoroutine(Slots02());
        }

        if (eventPos == 3)
        {
            StartCoroutine(Slots03());
        }

        if (eventPos == 4)
        {
            StartCoroutine(BobDisappear());
        }

        if (eventPos == 6)
        {
            StartCoroutine(Roulette02());
        }
        if (eventPos == 7)
        {
            StartCoroutine(Roulette03());
        }
        if (eventPos == 8)
        {
            StartCoroutine(Roulette04());
        }
        if (eventPos == 9)
        {
            StartCoroutine(Roulette05());
        }
        if (eventPos == 10)
        {
            StartCoroutine(Roulette06());
        }
    }
}
