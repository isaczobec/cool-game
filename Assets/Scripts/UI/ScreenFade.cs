using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{

    [Header("Animator variables")]
    [SerializeField] private Animator animator;
    [SerializeField] private string fadeOutRef = "fadeOut";
    [SerializeField] private string fadeInRef = "fadeIn";

    public event EventHandler fadeInFinnished;


    public static ScreenFade Instance{get; private set;}
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    public void FadeInFinnished() {
        fadeInFinnished?.Invoke(this, EventArgs.Empty);
    }

    public void StartFadeIn() {
        animator.SetTrigger(fadeInRef);
    }
    public void StartFadeOut() {
        animator.SetTrigger(fadeOutRef);
    }

}
