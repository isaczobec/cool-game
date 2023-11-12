using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitInfo
{
    
    public IEntity hurtEntity;
    public IEntity attackingEntity;
    public Projectile projectile;


    /// <summary>
    /// how much hard set damage this hit dealt. Is the first step in calculating the final damage of the hit. 
    /// </summary>
    public float baseDamage;
    
    /// <summary>
    /// how much damage this hit ultimately will deal. Should be the final step of damage calculation.
    /// </summary>
    public float damage;

    public float invincibilityTime;


}
