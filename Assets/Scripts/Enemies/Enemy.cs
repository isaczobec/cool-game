using System.Collections;
using System.Collections.Generic;
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
    }

    public virtual void HandleAI() {

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
        Debug.Log("Ouch but enemy");
    }
}
