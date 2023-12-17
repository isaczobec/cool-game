using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmorBossVisual : EnemyVisual
{

    [SerializeField] private AudioManager audioManager;

    [SerializeField] private ArmorBoss armorBoss;

    [SerializeField] private Animator armorBossAnimator;


    [SerializeField] private ArmorBossAnimationEvents armorBossAnimationEvents;

    
    [Header("VFX")]
    [SerializeField] private GameObject slashVFXObject;
    [SerializeField] private GameObject starBurstVFXObject;

    

    public override void InitializeVisual() {
        armorBoss.ArmorBossChangedState += SetAnimatorState;
        armorBossAnimationEvents.fireSoulProjectile += AnimationEvents_FireSouldProjectile;
    }


    private void Update() {
        if (armorBoss.state == armorBoss.soulAttackState) {
            FacePlayer(5f,0.1f);

        }


    }


    

    /// <summary>
    /// Toggles the trigger event with the same string name as the armorbossstate.
    /// </summary>
    private void SetAnimatorState(object sender, string armorBossState) {
        armorBossAnimator.SetTrigger(armorBossState);
    }
    

    private void AnimationEvents_FireSouldProjectile(object sender, EventArgs e)
    {
        

        // play the slash vfx when this item is used
        //slashVFX.Slash();
        GameObject slash = Instantiate(slashVFXObject);
        slash.SetActive(true);


        // spawn a starburst vfx
        GameObject starBurst = Instantiate(starBurstVFXObject,transform);
        starBurst.SetActive(true);
        starBurst.transform.localPosition = Vector3.zero;
        starBurst.transform.parent = null;

        StarBurstVFX starBurstVFX = starBurst.GetComponent<StarBurstVFX>();
        starBurstVFX.Setup(transform.rotation.eulerAngles.y - 90f); 


        audioManager.Play("SwordSlash");
    }
}

