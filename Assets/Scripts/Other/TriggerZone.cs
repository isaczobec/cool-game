using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour, IZone
{


    public event EventHandler<EventArgs> playerEnteredTriggerZone;
    public event EventHandler<EventArgs> playerLeftTriggerZone;

    public void PlayerEntered()
    {
        playerEnteredTriggerZone?.Invoke(this,EventArgs.Empty);
    }

    public void PlayerLeft()
    {
        playerLeftTriggerZone?.Invoke(this,EventArgs.Empty);
    }

}
