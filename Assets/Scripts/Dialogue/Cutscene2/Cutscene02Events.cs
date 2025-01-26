using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene02Events : MonoBehaviour
{
     //References
    public GameObject fadeScreenIn;
    public GameObject textBox;

    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextObject;
    [SerializeField] GameObject charName;

    //Bobby
    [SerializeField] GameObject bobbyRed;
    [SerializeField] GameObject bobbyPunched;
    public Animator bobbyAnimator;
    public AudioSource bobbyAudioSource;

    //Fry
    [SerializeField] GameObject fryGuy;
    public Animator fryAnimator;
    public AudioSource fryAudioSource;

    //Wenda
    [SerializeField] GameObject wendaWhale;
    public Animator wendaAnimator;
    public AudioSource wendaAudioSource;

    //Punch
    [SerializeField] GameObject punchButton;
    [SerializeField] GameObject fist;
    [SerializeField] GameObject kids;

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
    // Helper method to control who is talking
void SetTalking(GameObject character, Animator animator, AudioSource audioSource, bool isTalking)
{
    if (isTalking)
    {
        animator.SetTrigger("Talk");
        animator.Play("Talk");
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
    }
    else
    {
        animator.SetTrigger("Idle");
        animator.Play("Idle");
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

// Modify DisplayText to specify who is talking
IEnumerator DisplayText(GameObject character, Animator animator, AudioSource audioSource)
{
    skipText = false;
    textRunning = true;
    isTalking = true;

    // Start talking for the current character
    SetTalking(character, animator, audioSource, true);

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

    // Stop talking for the current character
    SetTalking(character, animator, audioSource, false);

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
        textToSpeak = "Well what d'you know...";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(bobbyRed, bobbyAnimator, bobbyAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        
        eventPos = 1;
    }

    IEnumerator Event02()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        textToSpeak = "You've already made a fortune out of a simple card game.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(bobbyRed, bobbyAnimator, bobbyAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 2;
    }

    IEnumerator Event03()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        textToSpeak = "Say, how about we keep on playing, huh? You've got enough luck for another round do ya?";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(bobbyRed, bobbyAnimator, bobbyAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 3;
    }

    IEnumerator Event04()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        textToSpeak = "Tomorrow?";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(bobbyRed, bobbyAnimator, bobbyAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 4;
    }

    IEnumerator Event05()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        textToSpeak = "What the hell do you mean tomorrow?";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(bobbyRed, bobbyAnimator, bobbyAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 5;
    }


    IEnumerator Event07()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        textToSpeak = "You've got a lot of nerve coming into my casino, playing my game, and just leave with MY money!";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(bobbyRed, bobbyAnimator, bobbyAudioSource));
        yield return new WaitForSeconds(0.05f);
        punchButton.SetActive(true);
        eventPos = 7;
    }

    IEnumerator Event08()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        fist.SetActive(true);
        bobbyRed.SetActive(false);
        bobbyPunched.SetActive(true);

        textToSpeak = "Yeouch!";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(bobbyRed, bobbyAnimator, bobbyAudioSource));
        yield return new WaitForSeconds(0.05f);

        textBox.SetActive(false);
        punchButton.SetActive(false);
        fist.SetActive(false);

        yield return new WaitForSeconds(2f);
        


        //Fry enters scene

        fryGuy.SetActive(true);
        textBox.SetActive(true);
        bobbyPunched.SetActive(false);

        charName.GetComponent<TMPro.TMP_Text>().text = "Fry";

        textToSpeak = "Sorry for the rudeness.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(fryGuy, fryAnimator, fryAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);

        eventPos = 8;
    }

    IEnumerator Event09()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        textToSpeak = "He tends to get a bit heated whenever people leave with their winnings.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(fryGuy, fryAnimator, fryAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 9;
    }

    IEnumerator Event10()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        wendaWhale.SetActive(true);

        charName.GetComponent<TMPro.TMP_Text>().text = "Wenda";

        textToSpeak = "Serves him right.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(wendaWhale, wendaAnimator, wendaAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 10;
    }

    IEnumerator Event11()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        wendaWhale.SetActive(true);

        charName.GetComponent<TMPro.TMP_Text>().text = "Fry";

        textToSpeak = "Greetings, Wenda.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(fryGuy, fryAnimator, fryAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 11;
    }

    IEnumerator Event12()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        wendaWhale.SetActive(true);

        charName.GetComponent<TMPro.TMP_Text>().text = "Wenda";

        wendaAnimator.SetTrigger("LooksRight");

        textToSpeak = "Hey, Fry. I heard what happened all the way from the daycare.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(wendaWhale, wendaAnimator, wendaAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 12;
    }
    IEnumerator Event13()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        wendaWhale.SetActive(true);

        charName.GetComponent<TMPro.TMP_Text>().text = "Wenda";

        wendaAnimator.SetTrigger("Smile");

        textToSpeak = "You absolutely mooly-wopped the hell out of Bobby. Those muscles aren't for show, right? Haha!";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(wendaWhale, wendaAnimator, wendaAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 13;
    }

    IEnumerator Event14()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        wendaWhale.SetActive(true);

        charName.GetComponent<TMPro.TMP_Text>().text = "Fry";

        wendaAnimator.SetTrigger("LooksRight");

        textToSpeak = "I'm going to go and clock out. I can spend the rest of day snoozing in my loveable cave.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(fryGuy, fryAnimator, fryAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 14;
    }

    IEnumerator Event15()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        wendaWhale.SetActive(true);
        fryGuy.SetActive(false);

        charName.GetComponent<TMPro.TMP_Text>().text = "Wenda";

        textToSpeak = "Enjoy yourself, Fry Guy.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(wendaWhale, wendaAnimator, wendaAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 15;
    }
    IEnumerator Event16()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        wendaWhale.SetActive(true);
        fryGuy.SetActive(false);
        kids.SetActive(true);

        charName.GetComponent<TMPro.TMP_Text>().text = "Wenda";

        wendaAnimator.SetTrigger("LooksAtYou");

        textToSpeak = "Here are your kids, ma'am.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(wendaWhale, wendaAnimator, wendaAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 16;
    }
    IEnumerator Event17()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        wendaWhale.SetActive(true);
        fryGuy.SetActive(false);
        kids.SetActive(true);

        charName.GetComponent<TMPro.TMP_Text>().text = "Wenda";

        textToSpeak = "These little shits have been meddling with just about everything! I can't even turn my back for one second without one of them playing with the vodka bottles.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(wendaWhale, wendaAnimator, wendaAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 17;
    }

    IEnumerator Event18()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        wendaWhale.SetActive(true);
        fryGuy.SetActive(false);
        kids.SetActive(true);

        charName.GetComponent<TMPro.TMP_Text>().text = "Wenda";

        wendaAnimator.SetTrigger("LooksRight");

        textToSpeak = "I honestly don't know how you do it. Being a mother must be tough, huh?";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(wendaWhale, wendaAnimator, wendaAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 18;
    }
    IEnumerator Event19()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);

        wendaWhale.SetActive(true);
        fryGuy.SetActive(false);
        kids.SetActive(true);

        charName.GetComponent<TMPro.TMP_Text>().text = "Wenda";

        wendaAnimator.SetTrigger("Smile");

        textToSpeak = "Well, take care and come back next time (without the kids)! I gotta clean this place up.";
        currentTextLength = textToSpeak.Length;

        yield return StartCoroutine(DisplayText(wendaWhale, wendaAnimator, wendaAudioSource));
        yield return new WaitForSeconds(0.05f);
        nextButton.SetActive(true);
        eventPos = 19;
    }

    IEnumerator Event20()
    {
        nextButton.SetActive(false);
        textBox.SetActive(true);
        fadeScreenOut.SetActive(true);
        yield return new WaitForSeconds(2);

   
        SceneManager.LoadScene(5);
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
       
            StartCoroutine(Event07());


        }
        if (eventPos == 8)
        {
            StartCoroutine(Event09());
        }
        if (eventPos == 9)
        {
            StartCoroutine(Event10());
        }
        if (eventPos == 10)
        {
            StartCoroutine(Event11());
        }
        if (eventPos == 11)
        {
            StartCoroutine(Event12());
        }
        if (eventPos == 12)
        {
            StartCoroutine(Event13());
        }
        if (eventPos == 13)
        {
            StartCoroutine(Event14());
        }
        if (eventPos == 14)
        {
            StartCoroutine(Event15());
        }
        if (eventPos == 15)
        {
            StartCoroutine(Event16());
        }
        if (eventPos == 16)
        {
            StartCoroutine(Event17());
        }
        if (eventPos == 17)
        {
            StartCoroutine(Event18());
        }
        if (eventPos == 18)
        {
            StartCoroutine(Event19());
        }
        if (eventPos == 19)
        {
            StartCoroutine(Event20());
        }
    }

    public void PunchButton() {

        if (eventPos == 7)
        {
            StartCoroutine(Event08());
        }

    }
}
