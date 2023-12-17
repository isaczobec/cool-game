using System;
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
        state = zoomMovingState;

        moveDestination = GetRandomZoomPosition();
    }


    public override void HandleAI() {
        if (state == zoomMovingState) {
            HandleZooming(moveDestination);
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
        FireSoulProjectile();
    }

    private void AnimationEvent_StoppedAttacking(object sender, System.EventArgs e)
    {

        // Initiate zooming state after the boss finnishes an attack
        moveDestination = GetRandomZoomPosition();
        state = zoomMovingState;
        ArmorBossChangedState?.Invoke(this, zoomMovingState);
    }

    private void FireSoulProjectile() {
        GameObject projectileObject = Instantiate(soulProjectile);
        FireBallProjectile projectile = projectileObject.GetComponent<FireBallProjectile>();
        projectile.SetOwner(null, this);
        projectile.ownerEntity = this;
        projectile.Initialize();
    }


    



}
