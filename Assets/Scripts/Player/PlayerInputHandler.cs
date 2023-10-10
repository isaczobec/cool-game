using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    private PlayerMovementControls playerMovementControls;

    
    public event EventHandler<OnPlayerJumpEventArgs> onPlayerJumpEvent;
    public class OnPlayerJumpEventArgs {
        public bool startedJumping;
    }
    
    private void Awake() {
        playerMovementControls = new PlayerMovementControls();
        playerMovementControls.NormalMovement.Enable();
        playerMovementControls.NormalMovement.Jump.performed += JumpStarted;
        playerMovementControls.NormalMovement.Jump.canceled += JumpCanceled;
    }




    private void JumpStarted(InputAction.CallbackContext context)
    {
        onPlayerJumpEvent?.Invoke(this,new OnPlayerJumpEventArgs{startedJumping = true});
    }
    private void JumpCanceled(InputAction.CallbackContext context)
    {
        onPlayerJumpEvent?.Invoke(this,new OnPlayerJumpEventArgs{startedJumping = false});
    }

    public Vector2 GetPlayerMovementVector() {
        return playerMovementControls.NormalMovement.Move.ReadValue<Vector2>();
    }

    

    

}
