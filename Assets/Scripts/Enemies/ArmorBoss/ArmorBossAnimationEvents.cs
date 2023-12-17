using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBossAnimationEvents : MonoBehaviour
{

    public event EventHandler fireSoulProjectile;
    public event EventHandler finnishedAttacking;



    

    public void FireSoulProjectile() {
        fireSoulProjectile?.Invoke(this, EventArgs.Empty);
        
    }
    public void FinnishedAttacking() {
        finnishedAttacking?.Invoke(this, EventArgs.Empty);
    }

}
