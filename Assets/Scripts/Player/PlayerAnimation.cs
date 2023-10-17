using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    
    [SerializeField] private Player player;
    [SerializeField] private PlayerInputHandler playerInputHandler;

    [SerializeField] private Animator animator;

    [SerializeField] private Transform visualParentTransform; //the visualobject parent transform of the model


    


    [Header("Animation Variables")]
    [Header("Base movement Variables")]
    [SerializeField] float turnAroundSpeed = 14f;

    private string isRunning = "isRunning";
    private string isGrounded = "isGrounded";
    private string noHorizontalInputTime = "noHorizontalInputTime";
    private string playerJumped = "playerJumped";

    private string xVelocityPercent = "xVelocityPercent"; //how much of the max x momentum the player is currently moving at




    [Header("Attacking Variables")]
    private string isAttacking = "isAttacking";

    [SerializeField] private float attackingWheightChangeSpeed = 8f;


    [SerializeField] private float attackingSpeed = 5f;

    
    

    
    
    
    //animator layer indexes




    [SerializeField] private int baseMovementLayerIndex = 0;
    [SerializeField] private int attackingLayerIndex = 1;

    


    
    private Vector2 playerMovementVector;

    public void Start() {

        player.OnPlayerJumped += Player_OnJumpedEvent;


    }


    private void Update() {


            playerMovementVector = playerInputHandler.GetPlayerMovementVector();


            //set animator variables for movement
            animator.SetBool(isRunning,player.GetIsRunning());
            animator.SetBool(isGrounded,player.GetIsGrounded());

            animator.SetFloat(xVelocityPercent,MathF.Abs(player.GetVelocity().x/player.GetMaxGroundedMovementSpeed())); //set velocitypercent

            animator.SetBool(isAttacking,player.GetPlayerIsAttacking());



            UpdatePlayerOrientation(playerMovementVector);
            HandleNoInputTime(playerMovementVector);

            UpdateAttacking();







    }

    private void UpdateAttacking()
    {
        float weightChange;  
        if (player.GetPlayerIsAttacking()) {
            weightChange = 1f;
        } else {
            weightChange = -1f;
        }



        float newAttackingLayerWheight = Mathf.Clamp(weightChange * attackingWheightChangeSpeed * Time.deltaTime + animator.GetLayerWeight(attackingLayerIndex),0f,1f); //the wheight of an animator layer can at max be 1, min 0
            animator.SetLayerWeight(attackingLayerIndex,newAttackingLayerWheight);
    }




    private void Player_OnJumpedEvent(object sender, EventArgs e)
    {
        animator.SetTrigger(playerJumped);
    }

    

    private void UpdatePlayerOrientation(Vector2 movementVector) {

        if (player.GetPlayerIsAttacking()) { // if the player is currently attacking, change their direction to whichever way they are attacking

            Vector3 slerpDirection;
            if (MouseInfo.Instance.GetMousePixelPosition().x > 0) {
                slerpDirection = Vector3.right;
            } else {

                slerpDirection = Vector3.left;
            }

        visualParentTransform.forward = Vector3.Slerp(visualParentTransform.forward,slerpDirection,Time.deltaTime * turnAroundSpeed);


        } else { // else change their direction to the direction they are moving



        Vector3 xInputVector = movementVector * Vector3.right;

        visualParentTransform.forward = Vector3.Slerp(visualParentTransform.forward,xInputVector,Time.deltaTime * turnAroundSpeed);

        }
    }


    private void HandleNoInputTime(Vector2 movementVector) { // used to not stop the running animation if the player stops inputting walk for a minimal time

        if (movementVector.x == 0) { // if the player is not moving hotizontally, update the no
        float temp_noHorizontalInputTime =  animator.GetFloat(noHorizontalInputTime);
        temp_noHorizontalInputTime += Time.deltaTime;
        animator.SetFloat(noHorizontalInputTime,temp_noHorizontalInputTime);
        } else {
        animator.SetFloat(noHorizontalInputTime,0f);
        }


    }



    
}
