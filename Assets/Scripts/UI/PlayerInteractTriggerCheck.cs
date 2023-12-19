using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractTriggerCheck : MonoBehaviour
{

    [SerializeField] private LayerMask interactZoneLayerMask;

    private InteractZone currentInteractZone = null;

    void Start()
    {

        PlayerInputHandler.Instance.interact += InteractKeyPressed;
        
    }

    private void InteractKeyPressed(object sender, EventArgs e)
    {
        if (currentInteractZone != null) {
            currentInteractZone.ZoneInteractedWith();
        }
    }


    private void OnTriggerEnter(Collider otherCollider)
    {
        CheckAndHandleCollision(otherCollider,entered:true);
    }
    private void OnTriggerExit(Collider otherCollider)
    {
        CheckAndHandleCollision(otherCollider,entered:false);
    }

    //checks if the trigger that was entered was an interactzone and runs the enter function in that case
    private void CheckAndHandleCollision(Collider otherCollider, bool entered)
    {
        if (interactZoneLayerMask == (interactZoneLayerMask | (1 << otherCollider.transform.gameObject.layer)))
        {// if the collided with object is on the same layer as this one

            if (otherCollider.gameObject.TryGetComponent<InteractZone>(out InteractZone interactZone))
            {
                if (entered) {
                    interactZone.PlayerEntered();
                    currentInteractZone = interactZone;
                } else {
                    interactZone.PlayerLeft();
                    currentInteractZone = null;
                }
            }

        }
    }
}
