using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    [SerializeField] private IHittableEntity ownerEntity; // the owner of this hitbox
    
    [SerializeField] private bool isPlayer;


    [SerializeField] private Projectile projectile; // the projectile this hitbox is attached to, null if there is not projectile asociated with this object


    [SerializeField] private LayerMask layerMask;


    [SerializeField] private Collider hitboxCollider; // the collider component associated with this hitbox

    /// <summary>
    /// called when this hitbox hits something else
    /// </summary>
    /// <param name="otherCollider"> The Hurtbox which was hit </param>
    private void OnTriggerStay(Collider otherCollider) {

        if (layerMask == (layerMask | (1 << otherCollider.transform.gameObject.layer)))  {// if the collided with object is on the same layer as this one
            if (otherCollider.TryGetComponent<Hurtbox>(out Hurtbox hurtbox)) {
                if (hurtbox.getIsPlayer() != isPlayer) { // if the hitbox belongs to the player and hits the player (or vice versa); do nothing

                    IHittableEntity hurtEntity = hurtbox.getOwnerEntity();
                        if (hurtEntity.GetInvincibilityTime() <= 0) { //checks if the hurt entity still has invicibility frames
                            HitInfo hitInfo = GetHitInfo(hurtEntity);
                            


                            hurtEntity.GetHit(hitInfo); //makes the HittableEntity get hit
                            projectile?.HitSomething(hitInfo); // sends hit info to the projectile that hit something
                        }

                }
            }
        }
    }


    private HitInfo GetHitInfo(IHittableEntity hurtEntity) {
        if (projectile != null) {
            Debug.Log(projectile);
            HitInfo hitInfo = projectile.GetHitInfo();
            hitInfo.attackingEntity = ownerEntity;
            hitInfo.hurtEntity = hurtEntity;
            return hitInfo;
        } else {
            return null; // placeholder until i figure out if any attacks wont have projectiles attached to them
        }
    }

    public void DisableHitbox() {
        hitboxCollider.enabled = false;
    }

    public void EnableHitbox() {
        hitboxCollider.enabled = true;
    }

    public bool getIsPlayer() {
        return isPlayer;
    }

}
