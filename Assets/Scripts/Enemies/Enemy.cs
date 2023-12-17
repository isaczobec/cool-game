using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Enemy : MonoBehaviour, IEntity
{

    public Player player;


    [SerializeField] public float maxHealth;

    /// <summary>
    /// variable attack damage can be multiplied with for scaling.
    /// </summary>
    [SerializeField] public float baseDamage;
    private float health;


    /// <summary>
    /// time in seconds the enemy still cannot be hit.
    /// </summary>
    private float invincibilityTime;


    // Start is called before the first frame update
    void Start()
    {

        player = Player.Instance;

        health = maxHealth;

        InitializeEnemy();
        
    }

    /// <summary>
    /// Base class method that is called in the start method.
    /// </summary>
    public virtual void InitializeEnemy() {
    }

    // Update is called once per frame
    void Update()
    {
        HandleAI();
        HandleDeath();
        HandleInvincibilityTime();
    }

    private void HandleInvincibilityTime() {
        invincibilityTime -= Time.deltaTime;
    }

    public virtual void HandleAI() {

    }

    /// <summary>
    /// Base class method that checks if the entity should die this frame. Runs every frame.
    /// </summary>
    public virtual void HandleDeath() {
        if (health <= 0) {
            Destroy(gameObject); // destroys this enemy
        }
    }



    public float GetHealth() {
        return health;
    }
    public float GetMaxHealth() {
        return maxHealth;
    }
    public float SetHealth() {
        return health;
    }
    public float SetMaxHealth() {
        return maxHealth;
    }

    public void GetHit(HitInfo hitInfo)
    {
        health -= hitInfo.damage;
        invincibilityTime = hitInfo.invincibilityTime;
        Debug.Log("Ouch but enemy");
    }

    public float GetInvincibilityTime() {
        return invincibilityTime;
    }

    public void SetInvincibilityTime(float invincibilityTime) {
        this.invincibilityTime = invincibilityTime;
    }

    public Transform GetTransform() {
        return transform;
    }

}
