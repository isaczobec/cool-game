using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PlayerInputHandler : MonoBehaviour
{


    private PlayerControls playerMovementControls;

    
    public event EventHandler<OnPlayerJumpEventArgs> onPlayerJumpEvent;
    public class OnPlayerJumpEventArgs {
        public bool startedJumping;
    }


    public event EventHandler<EventArgs> inventoryToggled;
    public event EventHandler<EventArgs> interact;

    public event EventHandler<bool> onPlayerAttackEvent; // the bool is if the player started attaqcking or not

    public event EventHandler<EventArgs> dashEvent; // the bool is if the player started attaqcking or not

    public static PlayerInputHandler Instance {get; private set;}
    
    private void Awake() {
        playerMovementControls = new PlayerControls();
        playerMovementControls.NormalMovement.Enable();
        playerMovementControls.NormalMovement.Jump.performed += JumpStarted;
        playerMovementControls.NormalMovement.Jump.canceled += JumpCanceled;
        playerMovementControls.NormalMovement.Attack.performed += AttackStarted;
        playerMovementControls.NormalMovement.Attack.canceled += AttackCanceled;
        playerMovementControls.NormalMovement.Dash.performed += DashPerformed;


        playerMovementControls.UI.Enable();
        playerMovementControls.UI.Inventory.performed += InventoryToggled;
        playerMovementControls.UI.Interact.performed += InteractPressed;

        Instance = this;
        

    }

    private void DashPerformed(InputAction.CallbackContext context)
    {
        dashEvent?.Invoke(this, EventArgs.Empty);
    }

    private void InteractPressed(InputAction.CallbackContext context)
    {
        interact?.Invoke(this, EventArgs.Empty);
    }

    private void InventoryToggled(InputAction.CallbackContext context)
    {
        inventoryToggled?.Invoke(this, EventArgs.Empty);
    }

    private void AttackStarted(InputAction.CallbackContext context)
    {
        onPlayerAttackEvent?.Invoke(this,true);
    }

    private void AttackCanceled(InputAction.CallbackContext context)
    {
        onPlayerAttackEvent?.Invoke(this,false);
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

  

    public float GetPlayerAttackInput() {
        return playerMovementControls.NormalMovement.Attack.ReadValue<float>();
    }

    

    

}
