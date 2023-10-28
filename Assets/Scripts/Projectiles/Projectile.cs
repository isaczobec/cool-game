using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{


    /// <summary>
    /// the player this projectile belongs to. Null if it does not belong to the player.
    /// </summary>
    public Player player; 
    /// <summary>
    /// the enemy this projectile belongs to. Null if it does not belong to an enemy.
    /// </summary>
    public Enemy enemy; 

    /// <summary>
    /// the item this projectile was created by or is associated with. Null if it is not associated an item.
    /// </summary>
    public Item parentItem; 

    

    

    // Update is called once per frame
    private void Update()
    {
        UpdateProjectile();
    }

    private void Start() {
        OnProjectileCreated();
    }


    /// <summary>
    /// Parent Class method that should be called DIRECTLY AFTER the projectile prefab is initialized. 
    /// </summary>
    /// <param name="player"></param>
    /// <param name="enemy"></param>
    public void SetOwner(Player player = null, Enemy enemy = null) {

        if (player != null && enemy != null) {
            Debug.LogError("A projectile can not be owned by two entities at once!");
        } else {
            this.player = player;
            this.enemy = enemy;
        }


        }

    

    
    /// <summary>
    /// Parent class method that is called when the projectile is first created. Can be used to initialize the projectile.
    /// </summary>
    public virtual void OnProjectileCreated() {

    }





    /// <summary>
    /// Parent class method that is called every frame. Updates the projectile.
    /// </summary>
    public virtual void UpdateProjectile() {

    }


    /// <summary>
    /// Called when this projectile hits something. 
    /// </summary>
    /// <param name="hitInfo">Class that passes on all info for the hit to the regarded classes.</param>
    public virtual void HitSomething(HitInfo hitInfo) {
        
    }

    /// <summary>
    /// Parent class method that is called when this projectile hits something. passes information onto what was hit so that damage can be taken, etc. Does not need to set the hurtEntity or attackingEntity as that is done in the hitbox class.
    /// </summary>
    public virtual HitInfo GetHitInfo() { // placeholder method that will be replaced in each individual subclass
        HitInfo hitInfo = new HitInfo();
        if (parentItem != null) {
            hitInfo.damage = parentItem.GetItemData().damage;
        }
        return hitInfo;
    }


    /// <summary>
    /// Sets the velocity of the projectile to travel towards the cursor. 
    /// </summary>
    /// <param name="travelSpeed">The scalar of the velocity vector.</param>
    /// <returns></returns>
    public Vector2 GetVelocityToCursor(float travelSpeed) {
        if (player != null) {

            return MouseInfo.GetPlayerMouseDirectionVector() * travelSpeed;
            
        } else {
            return Vector2.zero;
        }
    }

}
