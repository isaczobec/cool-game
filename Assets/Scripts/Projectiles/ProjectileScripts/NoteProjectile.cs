using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class NoteProjectile : Projectile
{

    [SerializeField] private float travelSpeed = 1f;

    private Vector2 velocity = Vector2.zero;

    [SerializeField] private float maxLifeTime = 10;


    

    
    /// <summary>
    /// Initializes this projectile. Should be called DIRECTLY AFTER the owner of this projectile is set. 
    /// </summary>
    public void Initialize() {

        transform.position = player.transform.position;

        velocity = GetVelocityToCursor(travelSpeed);
        Debug.Log(velocity);
        
    }

    public override void UpdateProjectile()
    {
        transform.position += new Vector3(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime, 0f);

        if (lifeTime > maxLifeTime) {
            HitInfo empty = new HitInfo();
            base.HitSomething(empty);
        }

    }

    public override void HitSomething(HitInfo hitInfo)
    {
        base.HitSomething(hitInfo);
        velocity = Vector2.zero;
        hitbox.DisableHitbox();
        
    }




}
