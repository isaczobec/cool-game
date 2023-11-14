using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{

    [SerializeField] private Player player;

    public event EventHandler<EventArgs> playerUsedItem; //placeholder, ska förmodligen ha med något mer här sen
    public event EventHandler<EventArgs> playerStoppedAttacking; 

    public void UsedItem() {
        playerUsedItem?.Invoke(this, EventArgs.Empty);
    }

    public void PlayPlayerItemUseSound() {
        Item item = player.GetEquippedItem();
        item.audioManager.Play(item.useSoundName);
    }

    public void PlayerEndedAttack() {
        playerStoppedAttacking?.Invoke(this, EventArgs.Empty);
    }

}
