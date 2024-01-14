using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractZone :  MonoBehaviour, IZone
{

    /// <summary>
    /// The text that will pop up when the player gets close to this box
    /// </summary>
    [SerializeField] private string interactText;

    private bool hidden = true;
    private bool zoneEnabled = true;
    private bool playerInZone = true;

    [SerializeField] private bool hideOnIteract = true;


    [SerializeField] private TextMeshProUGUI textMeshPro;

    [Header("Optional Variables")]
    [SerializeField] private bool switchScene; // if this interactzone should switch to another scene when pressed
    [SerializeField] private string sceneName;
    [SerializeField] private bool stopMusic = false;
    [SerializeField] private Vector3 playerSpawnPosition; 
    [SerializeField] private bool playDialouge; // if this interactzone should play dialouge when interacted with
    [SerializeField] private string dialougeLineGroupName; // the name of the dialouge group that should be played

    private DialougeLineGroups dialougeLineGroups;




    [Header("Animation Variables, reused inv animations")]
    [SerializeField] private Animator animator;
    [SerializeField] private string toggledReferenceString = "inventoryToggled"; // used inventory animations

    [Header("Sound Variables")] 
    [SerializeField] private AudioManager audioManager;

    /// <summary>
    /// event that is called when this box is clicked
    /// </summary>
    public event EventHandler InteractZoneClicked;

    private void Start() {
        textMeshPro.text = interactText;

        // set the dialouge groups
        if (playDialouge) {
            dialougeLineGroups = GetComponent<DialougeLineGroups>();
            DialougeBubble.Instance.DialougeBubbleClosed += DialougeBubbleClosed;
        }
    }

    private void DialougeBubbleClosed(object sender, EventArgs e)
    {
        Debug.Log("CLOSED!");
        if (playerInZone) {
            
            // make the interact button pop back up but it doesnt work

        }
    }
    

    

    public void ZoneInteractedWith() {

        if (!hidden && zoneEnabled) {
            InteractZoneClicked?.Invoke(this,EventArgs.Empty);

            if (hideOnIteract) {
                ToggleUIElemens(show:false);
                audioManager.Play("Interacted");

            }

            if (switchScene) {SwitchScene();}

            if (playDialouge) {PlayDialouge();}

        }

    }

    private void PlayDialouge() {
        dialougeLineGroups.QueueDialougeLines(dialougeLineGroupName,playInstantly: true);
    }

    private void SwitchScene() {
        if (stopMusic) {
            SoundTrackHandler.Instance.StopAllSoundTracks();
        }
        LevelManager.Instance.LoadScene(sceneName,playerSpawnPosition);
    }

    /// <summary>
    /// Function that is called when the player enters this zone trhough a script on the player.
    /// </summary>
    /// <param name="collider"></param>
    public void PlayerEntered() {
        playerInZone = true;

        if (zoneEnabled) {
            audioManager.Play("Entered");
            ToggleUIElemens(show:true);
        }
    }

    /// <summary>
    /// function that is run when the playr leaves this zone
    /// </summary>
    public void PlayerLeft() {
        playerInZone = false;

        if (zoneEnabled) { 
            if (!hidden) {audioManager.Play("Left");}
            ToggleUIElemens(show:false);
        }
    }

    private void ToggleUIElemens(bool show) {
        if (hidden && show) {

            animator.SetTrigger(toggledReferenceString);
            hidden = false;
        } else if (!hidden && !show) {
            animator.SetTrigger(toggledReferenceString);
            hidden = true;
            
        }
    }

    public void SetZoneEnabled(bool enabled) {
        zoneEnabled = enabled;
        ToggleUIElemens(show:enabled);
    }

}
