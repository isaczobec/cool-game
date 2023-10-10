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
    [SerializeField] float turnAroundSpeed = 14f;

    private string isRunning = "isRunning";
    private string isGrounded = "isGrounded";
    private string noHorizontalInputTime = "noHorizontalInputTime";
    private string playerJumped = "playerJumped";

    private string xVelocityPercent = "xVelocityPercent"; //how much of the max x momentum the player is currently moving at

    


    
    private Vector2 playerMovementVector;

    public void Start() {

        player.OnPlayerJumped += Player_OnJumpedEvent;

    }

    private void Player_OnJumpedEvent(object sender, EventArgs e)
    {
        animator.SetTrigger(playerJumped);
    }

    private void Update() {

            playerMovementVector = playerInputHandler.GetPlayerMovementVector();

            animator.SetBool(isRunning,player.GetIsRunning());
            animator.SetBool(isGrounded,player.GetIsGrounded());

            UpdatePlayerOrientation(playerMovementVector);
            HandleNoInputTime(playerMovementVector);



            animator.SetFloat(xVelocityPercent,MathF.Abs(player.GetVelocity().x/player.GetMaxGroundedMovementSpeed())); //set velocitypercent



    }
    

    private void UpdatePlayerOrientation(Vector2 movementVector) {

        Vector3 xInputVector = movementVector * Vector3.right;

        visualParentTransform.forward = Vector3.Slerp(visualParentTransform.forward,xInputVector,Time.deltaTime * turnAroundSpeed);
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
