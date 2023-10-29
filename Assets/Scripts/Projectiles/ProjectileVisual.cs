using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileVisual : MonoBehaviour
{

    /// <summary>
    /// The projectile this visualObject is associated with.
    /// </summary>
    [SerializeField] public Projectile projectile;
    /// <summary>
    /// The animator that controls this object  
    /// </summary>
    [SerializeField] public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        projectile.ProjectileHitSomething += Projectile_HitSomething;
        InitializeProjectileVisual();

    }

    public virtual void InitializeProjectileVisual() {

    } 

    public virtual void Projectile_HitSomething(object sender, EventArgs e)
    {
        animator.SetTrigger("hitSomething");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
