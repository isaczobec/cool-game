using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutsceneHandler : MonoBehaviour
{


    [SerializeField] private TriggerZone triggerZone;

    private bool cutSceneTriggered = false;

    [SerializeField] private Color cutSceneOverlayColor;
    [SerializeField] private float cutSceneOverlayColorFadeInTime = 1f;

    private DialougeLineGroups dialougeLineGroups;
    [SerializeField] private string dialougeGroup1Name;

    [SerializeField] private string dialougeEvent1;    
    


    // Start is called before the first frame update
    void Start()
    {

        dialougeLineGroups = GetComponent<DialougeLineGroups>();

        triggerZone.playerEnteredTriggerZone += TriggerZone_PlayerEnteredTriggerZone;
        triggerZone.playerLeftTriggerZone += TriggerZone_PlayerLeftTriggerZone;

        DialougeBubble.Instance.DialougeLineFinnished += DialougeLineFinnishedEvent;
    }

    private void DialougeLineFinnishedEvent(object sender, string e)
    {
        if (e == dialougeEvent1) {
         // do something       
        }
    }

    private void TriggerZone_PlayerLeftTriggerZone(object sender, EventArgs e)
    {
        ScreenTextureOverlay.Instance.FadeOut(cutSceneOverlayColorFadeInTime);
    }

    private void TriggerZone_PlayerEnteredTriggerZone(object sender, EventArgs e)
    {
        if (cutSceneTriggered == false) {
            StartCutScene();
        }
    }

    private void StartCutScene() {
        ScreenTextureOverlay.Instance.FadeToColor(cutSceneOverlayColor,cutSceneOverlayColorFadeInTime);
        dialougeLineGroups.QueueDialougeLines(dialougeGroup1Name,playInstantly:true);
    }

}
