using System;
using UnityEditor.UI;
using UnityEngine;



public class Player : MonoBehaviour, IEntity
{

    [SerializeField] private PlayerInputHandler playerInputHandler;


    [Header("Movement Variables")]
    [SerializeField] private float maxGroundedMovementSpeed = 10f;
    [SerializeField] private float groundedAcceleration = 4f;
    [SerializeField] private float groundedMovementDamping = 1f;
    [SerializeField] private float maxAerialMovementSpeed = 10f;
    [SerializeField] private float aerialAcceleration = 0.5f;
    [SerializeField] private float aerialMovementDamping = 1f;


    [Header("vertical movement Variables")]
    [SerializeField] private float aerialVerticalAcceleration = 6f;
    [SerializeField] private float maxAerialVerticalSpeed = 20f;
    [SerializeField] private float DownVerticalAcceleration = 4f; //gravity not included
    [SerializeField] private float maxDownVerticalSpeed = 20f;
    [SerializeField] private float downVerticalSpeedDamping = 3f;
    [SerializeField] private float gravityAcceleration = 3f;
    [SerializeField] private float MaxNaturalGravity = 10f;
    [SerializeField] private float jumpPower = 2f;
    
    [Header("movement cast variables")]
    [SerializeField] private Transform movementCastTransformStartPoint;
    [SerializeField] private Transform movementCastTransformEndPoint;
    [SerializeField] private float movementCastRadius = 0.5f;
    [SerializeField] private float skinwidth = 0.015f;

    [SerializeField] private float maxFloorDegrees = 45f;

    [Header("Ground Check Variables")]
    [SerializeField] private float GroundCheckRadius = 0.45f;
    [SerializeField] private float GroundCheckOffset = 0.1f;
    [SerializeField] private float GroundedDistanceOffset = 0.02f;


    [Header("Health Variables")]
    [SerializeField] private float maxHealth = 400f;
    private float health;


    [Header("Other")]
    [SerializeField] private int MovementColliderLayerInt = 6;
    private int MovementColliderLayerMask;
    [SerializeField] private Transform holdingTransform; // the object that held items will be parented to

    [SerializeField] private PlayerAnimationEvents playerAnimationEvents;



    [SerializeField] private GameObject testWeaponPrefab;

    [SerializeField] private Transform holdingBone;



    public event EventHandler<EventArgs> OnPlayerJumped;

    public event EventHandler<HitInfo> OnPlayerGotHit;

    



    private Vector2 velocity = Vector2.zero;

    private bool isGrounded = false;

    private float currentFloorAngle = 0f;

    private bool isRunning = false;

    /// <summary>
    /// time in seconds the player still cannot be hit.
    /// </summary>
    private float invincibilityTime;

    /// <summary>
    /// If the player is currently attacking or not.
    /// </summary>
    private bool isAttacking;



    private Item equipedItem; // the item the player is currently holding


    public static Player Instance {get; private set;}

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("there is more than one Player instance!");
        }
    }

    private void Start()
    {
        MovementColliderLayerMask = 1 << MovementColliderLayerInt;


        health = maxHealth;

        playerInputHandler.onPlayerJumpEvent+= OnJumpEvent;

        ChangeEquippedItem(testWeaponPrefab);

        playerAnimationEvents.playerUsedItem += PlayerUseEquippedItem;

        playerAnimationEvents.playerStoppedAttacking += PlayerStoppedAttacking;

    }


    void Update()
    {
        HandleMovement();
        HandleInvincibilityTime();

        UpdateAttacking();
    }

    private void HandleInvincibilityTime() {
        invincibilityTime -= Time.deltaTime;
    }


    private void HandleMovement() {
        


        CollisionDetectionOutput collisionDetection = HandleCollisions(transform.position, velocity);
        transform.position = collisionDetection.newPositionVector;
        velocity = collisionDetection.newVelocityVector;

        GroundCheck(collisionDetection.lastRaycastHit);
        
        UpdateVelocity();
        
       


    }

    private void UpdateVelocity() {

        Vector3 inputVector = playerInputHandler.GetPlayerMovementVector();


        if (inputVector.x != 0) {
            isRunning = true;
        } else {
            isRunning = false;
        }


        // add horizontal velocity
        float maxVelocity;
        float acceleration;
        float damping;
        if (isGrounded) {
            maxVelocity = maxGroundedMovementSpeed;
            acceleration = groundedAcceleration;
            damping = groundedMovementDamping;
        } else {
            maxVelocity = maxAerialMovementSpeed;
            acceleration = aerialAcceleration;
            damping = aerialMovementDamping;
        }

        Vector2 playerInputedXVelocityChange = inputVector * new Vector2(1,0) * acceleration * Time.deltaTime;

        if (playerInputedXVelocityChange.x != 0) {

        if (Mathf.Abs(playerInputedXVelocityChange.x + velocity.x) < maxVelocity || Mathf.Abs(playerInputedXVelocityChange.x + velocity.x) < Mathf.Abs(velocity.x)) {
        velocity += playerInputedXVelocityChange;
        }

        } else {
            //if the player isnt inputting anything, lower its velocity
            if (velocity.x > 0) {
                velocity.x -= damping * Time.deltaTime;
            }
            else if (velocity.x < 0) {
                velocity.x += damping * Time.deltaTime;
            } else {
                velocity.x = 0;
            }
        }



        // add vertical velocity
        Vector2 playerInputedYVelocityChange;
        if (inputVector.y > 0) { // if the player is holding up
            playerInputedYVelocityChange = inputVector * new Vector2(0,1) * aerialVerticalAcceleration * Time.deltaTime;
            if (maxAerialVerticalSpeed > velocity.y + playerInputedYVelocityChange.y) {
                velocity += playerInputedYVelocityChange;
            } 
        } else if (inputVector.y < 0) { // if the player is holding down
            playerInputedYVelocityChange = inputVector * new Vector2(0,1) * DownVerticalAcceleration * Time.deltaTime;
            if (-1 * maxDownVerticalSpeed < velocity.y + playerInputedYVelocityChange.y) {
                velocity += playerInputedYVelocityChange;
            }
        } else  { //if vertical input is nothing
            if (velocity.y < -1 * MaxNaturalGravity) { //reduce the players fall speed if it is falling faster then gravity and is not inputting anything
                velocity.y += downVerticalSpeedDamping * Time.deltaTime;
            }
        }


        if (!isGrounded) {
            if (velocity.y > -1 * MaxNaturalGravity) { // if the player isnt faller faster than max gravity, apply gravity
        velocity += Vector2.down*gravityAcceleration * Time.deltaTime;
            }
        }
    }

    public class CollisionDetectionOutput {
        public Vector3 newPositionVector;
        public Vector3 newVelocityVector;
        public RaycastHit lastRaycastHit;
    }
    private CollisionDetectionOutput HandleCollisions(Vector3 position, Vector3 velocity) {

       
        Vector3 originalPosition = position;

        float leftoverMagnitude = velocity.magnitude;
        Vector3 leftoverVelocity = velocity;

        RaycastHit raycastHit;
        RaycastHit previousRaycastHit = new RaycastHit();
        bool hitSomething = false;

        bool collisionDone = false;
        while ( collisionDone == false) {
            if (Physics.CapsuleCast(position + movementCastTransformStartPoint.localPosition,position + movementCastTransformEndPoint.localPosition,movementCastRadius + skinwidth,leftoverVelocity.normalized,out raycastHit,leftoverMagnitude*Time.deltaTime,layerMask: MovementColliderLayerMask)) {
                hitSomething = true;
                previousRaycastHit = raycastHit;


                Vector3 positionChangeVector = leftoverVelocity.normalized * (raycastHit.distance - skinwidth);
                position += positionChangeVector;
                leftoverMagnitude -= raycastHit.distance;
                leftoverVelocity = leftoverVelocity.normalized * leftoverMagnitude;
                leftoverVelocity = Vector3.ProjectOnPlane(leftoverVelocity,raycastHit.normal);

            }
            else {

                collisionDone = true;
                if (hitSomething) {

                    print("hit");
                    
                    //Calculate how much of the momentum should be kept after the collision
                    float velocityCollisionFactor = Mathf.Sin(Mathf.Deg2Rad*Vector3.Angle(previousRaycastHit.normal.normalized,velocity.normalized));


                    return new CollisionDetectionOutput{newPositionVector=position,newVelocityVector = leftoverVelocity.normalized * velocity.magnitude * velocityCollisionFactor,lastRaycastHit = previousRaycastHit};

                }

                return new CollisionDetectionOutput{newPositionVector = position + velocity * Time.deltaTime, newVelocityVector = velocity,lastRaycastHit = previousRaycastHit};

            }
        }

        return new CollisionDetectionOutput{newPositionVector = position + velocity * Time.deltaTime, newVelocityVector = velocity,lastRaycastHit = previousRaycastHit};

    }

    private void GroundCheck(RaycastHit lastRayCastHit) {

        


        if (Physics.SphereCast(transform.position + movementCastTransformStartPoint.localPosition,GroundCheckRadius,Vector3.down,out RaycastHit raycastHit,movementCastRadius - GroundCheckRadius + GroundCheckOffset,layerMask: MovementColliderLayerMask)) {

            float floorAngle = Vector3.Angle(raycastHit.normal.normalized,Vector3.up.normalized);

            if (floorAngle <= maxFloorDegrees) {

            
            //velocity.y = Mathf.Sin(Mathf.Deg2Rad*floorAngle) * velocity.y;

            currentFloorAngle = Vector3.Angle(raycastHit.normal.normalized,Vector3.up.normalized);

            
            if (raycastHit.distance >= GroundedDistanceOffset) {
            transform.position += Vector3.down * (raycastHit.distance - GroundedDistanceOffset);
            } else {
                transform.position += Vector3.up * (GroundedDistanceOffset - raycastHit.distance);
            }


        isGrounded = true;
        return;
            }
        }

        if (isGrounded) {
            velocity.y = Mathf.Tan(Mathf.Deg2Rad*currentFloorAngle) * velocity.x;
            RemoveGroundLock();
        } else {
        isGrounded = false;
        }
        
    }

    private void RemoveGroundLock(bool makeVelocityZero = false) {
        transform.position += Vector3.up * (movementCastRadius - GroundCheckRadius + GroundCheckOffset +GroundedDistanceOffset);
        isGrounded = false;

        if (makeVelocityZero == false) {
            velocity.y = 0f;
        }

    }



    private void OnJumpEvent(object sender, PlayerInputHandler.OnPlayerJumpEventArgs e)
    {
        if (e.startedJumping && isGrounded == true) {

        OnPlayerJumped?.Invoke(this,EventArgs.Empty);
        RemoveGroundLock(makeVelocityZero: true);
        velocity += Vector2.up * jumpPower;




        }
    }

    /// <summary>
    /// Sets the players equipped item to the one specefied in the agruments.
    /// </summary>
    /// <param name="newItem">The item the player equips.</param>
    public void ChangeEquippedItem(GameObject newItemPrefab) {
        GameObject newItemObject = Instantiate(newItemPrefab,holdingTransform);
        Item newItem = newItemObject.GetComponent<Item>();

        equipedItem = newItem;
        newItem.SetPlayer(this);

        newItemObject.transform.localPosition = newItem.GetHoldingTransform().localPosition * -1;
    }


    private void PlayerUseEquippedItem(object sender, EventArgs e)
    {
        equipedItem.PrimaryUse();
    }


    public bool GetIsGrounded() {
        return isGrounded;
    }
    public bool GetIsRunning() {
        return isRunning;
    }
    public Vector2 GetVelocity() {
        return velocity;
    }

    public float GetMaxGroundedMovementSpeed() {
        return maxGroundedMovementSpeed;
    }

    public Item GetEquippedItem() {
        return equipedItem;
    }

    public bool GetPlayerIsAttacking() {
        return isAttacking;
    }

    //
    private void UpdateAttacking() {
        if (playerInputHandler.GetPlayerAttackInput() > 0) { //a float that is either 1 or 0 depending on is the player is holding the attack button or not
            isAttacking = true;
        }
    }
    private void PlayerStoppedAttacking(object sender, EventArgs e)
    {
        if (playerInputHandler.GetPlayerAttackInput() == 0) { // if the player isnt inputting attack
        isAttacking = false;
        }
    }

    public void GetHit(HitInfo hitInfo)
    {
        if (hitInfo != null) {

            OnPlayerGotHit?.Invoke(this, hitInfo);

            health -= hitInfo.damage;
            invincibilityTime = hitInfo.invincibilityTime;
        }
    }

    public float GetHealthPercent() {
        return health/maxHealth;
    }


    public float GetInvincibilityTime() {
        return invincibilityTime;
    }

    public void SetInvincibilityTime(float invincibilityTime) {
        this.invincibilityTime = invincibilityTime;
    }


    /// <summary>
    /// get the bone the player uses to hold items.
    /// </summary>
    /// <returns></returns>
    public Transform GetHoldingBone() {
        return holdingBone;
    }

    public Transform GetTransform() {
        return transform;
    }

    
    
}
