using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingTextManager : MonoBehaviour
{


    [SerializeField] private Animator animator;

    [SerializeField] private string toggledReference = "inventoryToggled";

    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.sceneLoadInitiated += OnSceneLoadInitiated;
        LevelManager.Instance.sceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(object sender, EventArgs e)
    {
        Debug.Log("SetOn");
        ToggleLoadingText(false);
    }

    private void OnSceneLoadInitiated(object sender, EventArgs e)
    {
        Debug.Log("SetOff");
        ToggleLoadingText(true);
    }

    private void ToggleLoadingText(bool on)
    {
        if (isActive != on)
        {
            animator.SetTrigger(toggledReference);
            isActive = on;
        }
    }

}
