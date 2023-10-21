using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitInfo : MonoBehaviour
{
    
    public IHittableEntity HurtEntity;
    public IHittableEntity AttackingEntity;
    public Projectile projectile;

    public float damage;


}
