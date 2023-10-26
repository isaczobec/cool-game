using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{

    public event EventHandler<EventArgs> playerUsedItem; //placeholder, ska förmodligen ha med något mer här sen

    public void UsedItem() {
        playerUsedItem?.Invoke(this, EventArgs.Empty);
    }

}
