using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Properties;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UI;
using UnityEngine;

public class NoteProjectile : Projectile
{

    [SerializeField] private float travelSpeed = 1f;
    [SerializeField] private float hoamingStrength = 10f;

    private Vector2 velocity = Vector2.zero;



    [Header("Sound Variables")]
    [SerializeField] private string dizzySoundsString = "dizzySounds";
    [SerializeField] private float chanceToPlayDizzySounds = 0.2f;


    [Header("VFX")]
    [SerializeField] private GameObject StarBurstVFXObject;

    private Enemy targetEnemy; // the enemy this projectile will hoam towards


    

    
    /// <summary>
    /// Initializes this projectile. Should be called DIRECTLY AFTER the owner of this projectile is set. 
    /// </summary>
    public void Initialize() {

        transform.position = player.transform.position;

        velocity = GetVelocityToCursor(travelSpeed);
        Debug.Log(velocity);

        targetEnemy = EnemyManager.Instance.GetClosestEnemyTo(transform.position);
        
    }

    public override void UpdateProjectile()
    {

        if (targetEnemy != null && velocity != Vector2.zero) {
            velocity = Vector3.Slerp(velocity,(targetEnemy.transform.position - transform.position).normalized * travelSpeed,hoamingStrength * Time.deltaTime);
        } else {
            targetEnemy = EnemyManager.Instance.GetClosestEnemyTo(transform.position);
        }

        transform.position += new Vector3(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime, 0f);


    }

    public override void HandleLifeTimeExpired()
    {

        //disable the projectile and play the "death" animation if the lifetime expires
        visualGameObject.GetComponent<ProjectileVisual>().animator.SetTrigger("hitSomething");
        hitbox.DisableHitbox();
    }

    public override void HitSomething(HitInfo hitInfo)
    {
        GameObject starBurst = Instantiate(StarBurstVFXObject,transform);
        starBurst.SetActive(true);
        starBurst.transform.localPosition = Vector3.zero;
        starBurst.transform.parent = null;

        StarBurstVFX starBurstVFX = starBurst.GetComponent<StarBurstVFX>();
        starBurstVFX.Setup(MouseInfo.CorrectAtanForRotation(velocity.x,velocity.y,flipY: true)); 


        base.HitSomething(hitInfo);
        velocity = Vector2.zero;
        hitbox.DisableHitbox();

        audioManager.PlayRandom(dizzySoundsString,chanceToPlay: chanceToPlayDizzySounds);


        
    }




}
