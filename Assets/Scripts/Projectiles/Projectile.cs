using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    /// <summary>
    /// the entity which owns this projectile. Null if it isnt owned by any entity.
    /// </summary>
    public IEntity ownerEntity;


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

    /// <summary>
    /// the hitbox that is connected to this projectile.
    /// </summary>
    public Hitbox hitbox; 


    /// <summary>
    /// the amount of seconds this projectile has been alive
    /// </summary>
    public float lifeTime {get; private set;} 


    

    /// <summary>
    /// the amount of seconds the entity which got hit will be invincible.
    /// </summary>
    public float invincibilityTimeOnHit;


    /// <summary>
    /// the visual object parented to this object, used to display the projectiles graphics.
    /// </summary>
    [SerializeField] public GameObject visualGameObject;


    [Header("Audio")]

    /// <summary>
    /// The audiomanager used to play sounds for this projectile.
    /// </summary>
    [SerializeField] public AudioManager audioManager;

    [SerializeField] private string defaultHitSoundName = "defaultHitSound";
    [SerializeField] private string defaultHitSoundGroupName = "defaultHitSoundGroup";

    public event EventHandler<EventArgs> ProjectileHitSomething;
    

    

    // Update is called once per frame
    private void Update()
    {
        HandleLifetime();
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
    /// Parent class method that is called every frame. Updates the projectile. Conatains some logic that usually should be ran every frame
    /// </summary>
    public virtual void UpdateProjectile() {

    }


    /// <summary>
    /// Called when this projectile hits something. 
    /// </summary>
    /// <param name="hitInfo">Class that passes on all info for the hit to the regarded classes.</param>
    public virtual void HitSomething(HitInfo hitInfo) {
        Debug.Log(this);
        ProjectileHitSomething?.Invoke(this, EventArgs.Empty);

        CreateDamageNumber(hitInfo);

        // play hit sound
        audioManager.PlayRandom(defaultHitSoundGroupName);
        audioManager.Play(defaultHitSoundName);
    }

    /// <summary>
    /// Parent class method that is called when this projectile hits something. passes information onto what was hit so that damage can be taken, etc. Does not need to set the hurtEntity or attackingEntity as that is done in the hitbox class.
    /// </summary>
    public virtual HitInfo GetHitInfo() { // base class method that can be replaced in each individual subclass
        HitInfo hitInfo = new HitInfo();

        if (parentItem != null) {
            hitInfo.baseDamage = parentItem.GetItemData().damage;
            hitInfo.damage = CalculateDamage();
        } 
        else if (enemy != null) {
            hitInfo.baseDamage = enemy.baseDamage;
            hitInfo.damage = enemy.baseDamage;
        }

        hitInfo.invincibilityTime = invincibilityTimeOnHit;
        return hitInfo;
    }

    /// <summary>
    /// Calculates how much damage a projectile should do.
    /// </summary>
    /// <returns></returns>
    public virtual float CalculateDamage() {
        if (parentItem != null) {

            ItemData parentItemData = parentItem.GetItemData();

            float damageMultiplier = 1 - UnityEngine.Random.Range(-1f,1f) * parentItemData.damageVariance;
            return math.round(parentItemData.damage * damageMultiplier); // calcul
        } else {

            

            return 0;
        }
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

    /// <summary>
    /// Base class method that increases lifetime of a projectile. 
    /// </summary>
    private void HandleLifetime() {
        lifeTime += Time.deltaTime;
    }

    /// <summary>
    /// Creates a damage number popup at this projectiles location.
    /// </summary>
    public void CreateDamageNumber(HitInfo hitInfo) {
        GameObject damagePopup = Instantiate(TextHandler.Instance.GetDamagePopup());
        damagePopup.transform.localPosition = transform.position;
        damagePopup.GetComponent<DamagePopup>().Setup(hitInfo);
    }

}
