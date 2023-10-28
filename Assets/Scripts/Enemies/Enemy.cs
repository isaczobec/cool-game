using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Enemy : MonoBehaviour, IHittableEntity
{

    public Player player;


    [SerializeField] private float maxHealth;
    private float health;


    // Start is called before the first frame update
    void Start()
    {

        player = Player.Instance;

        health = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleAI();
        HandleDeath();
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
        Debug.Log("Ouch but enemy");
    }
}
