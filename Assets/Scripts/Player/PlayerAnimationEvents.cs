using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{

    [SerializeField] private Player player;

    [SerializeField] private FootStepVFX footStepVFX;


    public event EventHandler<EventArgs> playerUsedItem; //placeholder, ska förmodligen ha med något mer här sen
    public event EventHandler<EventArgs> playerStoppedAttacking; 

    [Header("Audio")]
    [SerializeField] private AudioManager playerAudioManager;
    [SerializeField] private string gravelFootsteps = "gravelFootsteps";

    public void UsedItem() {
        playerUsedItem?.Invoke(this, EventArgs.Empty);
    }

    public void PlayPlayerItemUseSound() {
        Item item = player.GetEquippedItem();
        item.audioManager.Play(item.useSoundName,UnityEngine.Random.Range(0.8f,1.2f));
    }

    public void PlayerEndedAttack() {
        playerStoppedAttacking?.Invoke(this, EventArgs.Empty);
    }

    public void LeftFootstep() {
        footStepVFX.CreateFootStepVFX(leftFoot: true);
        playerAudioManager.PlayRandom(gravelFootsteps, randomPitchFactor: 0.1f);
    }
    public void RightFootstep() {
        footStepVFX.CreateFootStepVFX(leftFoot: false);
        playerAudioManager.PlayRandom(gravelFootsteps, randomPitchFactor: 0.1f);
    }

}
