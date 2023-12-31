using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulProjectile : Projectile
{
    [SerializeField] private float travelSpeed = 20f;

    private Vector2 velocity;


        
    [SerializeField] private float hoamingStrength = 0.1f;
    [SerializeField] private float hoamingStrengthWhenFarAway = 0.2f;

    private float hoamingFactor;



    [SerializeField] private float maxDistBeforeAccel = 20f;
    [SerializeField] private float accelerationFactor = 0.08f;


    




    

    
    /// <summary>
    /// Initializes this projectile. Should be called DIRECTLY AFTER the owner of this projectile is set. 
    /// </summary>
    public void Initialize(Vector3 aimVelocity) {

        hoamingFactor = hoamingStrength;

        if (enemy != null) {
            transform.position = enemy.transform.position;
        } else {
            transform.position = player.transform.position;
        }

        velocity = aimVelocity.normalized;

        
    }

    public override void UpdateProjectile()
    {

        if (velocity != Vector2.zero) {
        velocity = GetHomingDirection() * velocity.magnitude;


        if ((transform.position - Player.Instance.transform.position).magnitude > maxDistBeforeAccel) {
            velocity += velocity * Time.deltaTime * accelerationFactor; // accelerate the projectile slightliy
            hoamingFactor = hoamingStrengthWhenFarAway;
        } else {
            hoamingFactor = hoamingStrength;
        }
        }

        //move projectile w velocity
        transform.position = transform.position + new Vector3(velocity.x * Time.deltaTime * travelSpeed, velocity.y * Time.deltaTime * travelSpeed, 0f);




    }

    private Vector3 GetHomingDirection() {

        Vector3 toPlayerDir = (Player.Instance.transform.position - transform.position).normalized;

        return Vector3.Slerp(velocity.normalized,toPlayerDir,hoamingFactor*Time.deltaTime);

    }

    public override void HandleLifeTimeExpired() {
            HitInfo empty = new HitInfo();

            //disable the projectile and play the "death" animation if the lifetime expires
            velocity = Vector2.zero;
            visualGameObject.GetComponent<ProjectileVisual>().animator.SetTrigger("hitSomething");
            hitbox.DisableHitbox();

    }

    public override void HitSomething(HitInfo hitInfo)
    {


        base.HitSomething(hitInfo);
        velocity = Vector2.zero;
        hitbox.DisableHitbox();



        
    }
}
