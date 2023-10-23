using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteProjectile : Projectile
{

    [SerializeField] float travelSpeed = 1f;

    private Vector2 velocity = Vector2.zero;

    
    /// <summary>
    /// Initializes this projectile. Should be called DIRECTLY AFTER the owner of this projectile is set. 
    /// </summary>
    public void Initialize() {
        velocity = GetVelocityToCursor(travelSpeed);
        Debug.Log(velocity);
    }

    public override void UpdateProjectile()
    {
        transform.position += new Vector3(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime, 0f);
    }

    public override void HitSomething(HitInfo hitInfo)
    {
        Destroy(gameObject);
    }



}
