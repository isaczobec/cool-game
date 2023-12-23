using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialougeBubble : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    
    [Header("Sound")]
    [Header("Scroll sound used by many objects")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string textScrollSoundName = "scrollSound";
    [SerializeField] private string bubbleAppearSoundName = "bubbleAppear";
    [SerializeField] private string bubbleDisappearSoundName = "bubbledisAppear";
    
    private Sound textScrollSound;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string uiBoxEnabled = "uiBoxEnabled";
    [SerializeField] private string uiBoxDisabled = "uiBoxDisabled";
    [SerializeField] private string uiBoxBlip = "uiBoxBlip";

    private List<DialougeLine> dialougeLineQueue = new List<DialougeLine>(); 


    private bool activated = false;

    private string targetText;

    /// <summary>
    /// How many seconds between when each character appears
    /// </summary>
    private float scrollSpeed;

    private float timeUntilNextCharacter;
    private int amountOfCharactersCurrentlyShowing;
    private int amountOfCharactersToShow;
    private bool currentlySkippable = true;

    private float timeUntilAutoProceed = 0f; // 0 if there is no auto proceed

    [Header("Text Variables")]
    [SerializeField] private float separatorTimeMultiplier = 3f;


    private string currentDialougeEventTag;
    public event EventHandler<string> DialougeLineFinnished;

    public static DialougeBubble Instance;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    void Start()
    {
        textScrollSound = audioManager.GetSound(textScrollSoundName);
        PlayerInputHandler.Instance.interact += PlayerClicked;
    }

    private void Update() {
        ScrollText();
        HandleAutoProceed();
    }

    private void HandleAutoProceed() {
        if (timeUntilAutoProceed != 0f) {
            if (amountOfCharactersCurrentlyShowing >= amountOfCharactersToShow) {

                timeUntilAutoProceed -= Time.deltaTime;
                if (timeUntilAutoProceed <= 0) {
                    NextDialougeLine();
                }
            }
        }
    }

    /// <summary>
    /// Shows the dialogeline line.
    /// </summary>
    public void PlayDialougeLine(DialougeLine dialougeLine) {

        // animate the bubble
        if (activated) {
            animator.SetTrigger(uiBoxBlip); 
        } else {
            animator.SetTrigger(uiBoxEnabled); 
            audioManager.Play(bubbleAppearSoundName);
            
        }


        // Change variables
        activated = true;

        amountOfCharactersCurrentlyShowing = 0;
        amountOfCharactersToShow = dialougeLine.text.Length;
        
        scrollSpeed = dialougeLine.scrollSpeed;
        targetText = dialougeLine.text;
        textMeshProUGUI.text = "";
        textMeshProUGUI.color = dialougeLine.color;
        textMeshProUGUI.font = dialougeLine.font;
        textMeshProUGUI.fontSize = dialougeLine.fontSize;
        textScrollSound.clip = dialougeLine.textScrollSound;
        Player.Instance.SetLetPlayerMove(!dialougeLine.freezePlayerMovement);
        currentlySkippable = dialougeLine.skippable;
        timeUntilAutoProceed = dialougeLine.timeUntilAutoProceed;

        currentDialougeEventTag = dialougeLine.dialougeEventTag;

        

    


    }

    private void ScrollText() {
        timeUntilNextCharacter -= Time.deltaTime;
        if (amountOfCharactersCurrentlyShowing < amountOfCharactersToShow) {
            if (timeUntilNextCharacter <= 0) {

                char newChar = targetText[amountOfCharactersCurrentlyShowing];

                textMeshProUGUI.text = textMeshProUGUI.text + newChar;
                amountOfCharactersCurrentlyShowing += 1;

                if (newChar == '!' || newChar == ',' || newChar == '.' || newChar == '?') {
                    timeUntilNextCharacter = scrollSpeed * separatorTimeMultiplier;
                } else {
                    timeUntilNextCharacter = scrollSpeed;
                }
                    
                if (newChar != ' ') {
                    audioManager.Play(textScrollSoundName);
                }
            }
        }
    }

    private void PlayerClicked(object sender, EventArgs e) {

        if (amountOfCharactersCurrentlyShowing < amountOfCharactersToShow) {

            if (amountOfCharactersCurrentlyShowing > 1 && currentlySkippable) { // only allow player to skip if 1 character is currently sjowing

                textMeshProUGUI.text = targetText;
                amountOfCharactersCurrentlyShowing = amountOfCharactersToShow;
            }


        } else
        {
            if (timeUntilAutoProceed <= 0) {
                NextDialougeLine();
            }
        }

    }

    private void NextDialougeLine()
    {

        if (currentDialougeEventTag != "") {
            DialougeLineFinnished?.Invoke(this, currentDialougeEventTag);
        }

        // disable the bubble as this was the last dialouge line
        if (dialougeLineQueue.Count == 1 && activated == true)
        {

            activated = false;
            animator.SetTrigger(uiBoxDisabled);
            audioManager.Play(bubbleDisappearSoundName);
            Player.Instance.SetLetPlayerMove(true);

        }
        else if (dialougeLineQueue.Count > 1)
        { // play the next dialouge box
            PlayDialougeLine(dialougeLineQueue[1]);
        }
        if (dialougeLineQueue.Count != 0)
        {
            dialougeLineQueue.RemoveAt(0);
        } // remove the current dialouge box from the queue 
    }


    /// <summary>
    /// Adds some dialouge line to the dialouge queue
    /// </summary>
    public void AddDialougeLinesToQueue(DialougeLine[] dialougeLines, bool playInstantly) {
        foreach (DialougeLine dialougeLine in dialougeLines) {
            dialougeLineQueue.Add(dialougeLine);
        }
        if (playInstantly) {
            PlayDialougeLine(dialougeLineQueue[0]);
        }
    }


    



}
