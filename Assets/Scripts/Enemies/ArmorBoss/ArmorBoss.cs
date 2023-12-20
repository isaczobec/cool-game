using System;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering.Fullscreen.ShaderGraph;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArmorBoss : Enemy
{


    [Header("Projectile References")]
    [SerializeField] private GameObject soulProjectile;


    [Header("Movement")]
    [SerializeField] private float movementSpeedFactor = 2f;

    [SerializeField] private float zoomMovementSpeedFactor = 1f;


    [Header("Other")]
    [SerializeField] private float difficultyFactor = 0.5f;
    [SerializeField] private ArmorBossAnimationEvents armorBossAnimationEvents;

    [SerializeField] private InteractZone talkInteractZone;

    private bool fullScale = false; // if the boss has reached full scale at the start of the fight
    [SerializeField] private float scaleUpSpeed = 5f;
    [SerializeField] private float ScaleUpToWhenFightBegins = 2f; // what the scale of the boss will be when the fight begins

    [Header("Attack Variables")]

    [SerializeField] private float soulProjectileAngleDifference = 35f;
    [SerializeField] private int soulProjectileBurstAmount = 5;
    

    /// <summary>
    /// Which phase the boss is currently in
    /// </summary>
    public string phase;
    public string firstPhase =  "FIRSTPHASE";


    /// <summary>
    /// Which state the boss is in (If it is moving, attacking, etc) 
    /// </summary>
    public string state;
    public string zoomMovingState = "ZOOMMOVING";
    public string soulAttackState = "SOULATTACK";
    public string unAgressiveState = "UNAGRESSIVE";


    /// <summary>
    /// General vector3 for the boss' general target location
    /// </summary>
    private Vector3 moveDestination;


    // Events

    public event EventHandler<string> ArmorBossChangedState;


    public override void InitializeEnemy() {

        armorBossAnimationEvents.finnishedAttacking += AnimationEvent_StoppedAttacking;
        armorBossAnimationEvents.fireSoulProjectile += AnimationEvent_FireSoulProjectile;

        phase = firstPhase;
        state = unAgressiveState;

        moveDestination = GetRandomZoomPosition();

        talkInteractZone.InteractZoneClicked += TalkedWith;
    }

    private void TalkedWith(object sender, EventArgs e)
    {
        state = zoomMovingState; // initiate the bossbattle
        ArmorBossChangedState?.Invoke(this, zoomMovingState);
        talkInteractZone.SetZoneEnabled(false);

    }

    public override void HandleAI() {
        if (state == zoomMovingState) {
            HandleZooming(moveDestination);
        }

        // scale up to full size
        if (fullScale == false && state != unAgressiveState) {HandleStartScaleUp();} 
    }

    /// <summary>
    /// Scales up the boss when the fight begins
    /// </summary>
    private void HandleStartScaleUp() {
        
        transform.localScale = transform.localScale + Time.deltaTime * scaleUpSpeed * Vector3.one;
        if (transform.localScale.x >= ScaleUpToWhenFightBegins) {
            transform.localScale = new Vector3(ScaleUpToWhenFightBegins,ScaleUpToWhenFightBegins,ScaleUpToWhenFightBegins);
            fullScale = true;
        }
    }

    /// <summary>
    /// Gets a random postition around the player for the boss to zoom to.
    /// </summary>
    /// <returns></returns>


    private float minimumZoomDistance = 0.1f;

    private void HandleZooming(Vector3 zoomDestination) {
        Vector3 newPostition = Vector3.Lerp(transform.localPosition, zoomDestination, Time.deltaTime * movementSpeedFactor * zoomMovementSpeedFactor);

        if ((newPostition - transform.position).magnitude < minimumZoomDistance) {
            state = soulAttackState;
            ArmorBossChangedState?.Invoke(this, soulAttackState);
        } else {
            transform.position = newPostition;
        }
    }


    private float zoomPlayerXOffset = 35f; // how far to the right or left of the player the boss will zoom to
    private float zoomPlayerYOffsetRange = 20f; // how far up or down from the player the boss will zoom to

    private Vector3 GetRandomZoomPosition() {
        Vector3 playerPos = Player.Instance.transform.localPosition;
        float zoomX = (Random.Range(1f,0f) * 2f - 1f) * zoomPlayerXOffset + playerPos.x;
        float zoomY = Random.Range(-zoomPlayerYOffsetRange,zoomPlayerYOffsetRange) + playerPos.y; 

        

        

        Vector3 zoomDestination = new Vector3(zoomX,zoomY,0f);

        Debug.Log(zoomDestination);

        return zoomDestination;

        
    }
    private void AnimationEvent_FireSoulProjectile(object sender, System.EventArgs e)
    {
        FireSoulProjectileBurst(soulProjectileBurstAmount,soulProjectileAngleDifference);
    }

    private void AnimationEvent_StoppedAttacking(object sender, System.EventArgs e)
    {

        // Initiate zooming state after the boss finnishes an attack
        moveDestination = GetRandomZoomPosition();
        state = zoomMovingState;
        ArmorBossChangedState?.Invoke(this, zoomMovingState);
    }


    /// <summary>
    /// Fires a simaltaneous burst of soul projectiles against the player
    /// </summary>
    private void FireSoulProjectileBurst(int burstAmount, float angleDifference) {

        Vector3 playerDir = Player.Instance.transform.position - transform.position;



        for (int i=0; i<burstAmount;i++) {
            

            Vector3 dir = Quaternion.AngleAxis(i * angleDifference - burstAmount*angleDifference/2,Vector3.forward) * playerDir;
            
            
            FireSoulProjectile(dir);
        }


        
        

    }


    /// <summary>
    /// Fire a single soul projectile in a direction vector
    /// </summary>
    /// <param name="direction"></param>
    private void FireSoulProjectile(Vector3 direction) {
        
        GameObject projectileObject = Instantiate(soulProjectile);
        SoulProjectile projectile = projectileObject.GetComponent<SoulProjectile>();
        projectile.SetOwner(null, this);
        projectile.ownerEntity = this;
        projectile.Initialize(direction);

    }


    



}
