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

    [SerializeField] private bool hideOnIteract = true;


    [SerializeField] private TextMeshProUGUI textMeshPro;

    [Header("Optional Variables")]
    [SerializeField] private bool switchScene; // if this interactzone should switch to another scene when pressed
    [SerializeField] private string sceneName;


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
    }

    public void ZoneInteractedWith() {

        if (!hidden && zoneEnabled) {
            InteractZoneClicked?.Invoke(this,EventArgs.Empty);

            if (hideOnIteract) {
                ToggleUIElemens(show:false);
                audioManager.Play("Interacted");

            }

            if (switchScene) {SwitchScene();}

        }

    }

    private void SwitchScene() {
        LevelManager.Instance.LoadScene(sceneName);
    }

    /// <summary>
    /// Function that is called when the player enters this zone trhough a script on the player.
    /// </summary>
    /// <param name="collider"></param>
    public void PlayerEntered() {

        if (zoneEnabled) {
            audioManager.Play("Entered");
            ToggleUIElemens(show:true);
        }
    }

    /// <summary>
    /// function that is run when the playr leaves this zone
    /// </summary>
    public void PlayerLeft() {
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
