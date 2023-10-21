using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    [SerializeField] private IHittableEntity ownerEntity; // the owner of this hitbox
    
    [SerializeField] private bool isPlayer;

    [SerializeField] private Collider collider;

    [SerializeField] private Projectile projectile; // the projectile this hitbox is attached to, null if there is not projectile asociated with this object


    [SerializeField] private LayerMask layerMask;

    private void OnTriggerEnter(Collider otherCollider) {

        if (layerMask == (layerMask | (1 << otherCollider.transform.gameObject.layer)))  {// if the collided with object is on the same layer as this one
            if (otherCollider.TryGetComponent<Hurtbox>(out Hurtbox hurtbox)) {
                if (hurtbox.getIsPlayer() != isPlayer) { // if the hitbox belongs to the player and hits the player; do nothing
                    hurtbox.getOwnerEntity().GetHit(GetHitInfo()); //makes the HittableEntity take damage
                }
            }
        }
    }


    private HitInfo GetHitInfo() {
        if (projectile != null) {
            return projectile.GetHitInfo();
        } else {
            return null; // placeholder until i figure out if any attacks wont have hitboxes attached to them
        }
    }

    public bool getIsPlayer() {
        return isPlayer;
    }

}
