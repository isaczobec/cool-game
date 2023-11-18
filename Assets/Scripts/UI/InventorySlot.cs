using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private Animator animator;

    [SerializeField] private string isHovering = "isHovered";
    [SerializeField] private string toggled = "toggled";



    public bool waitingToBeToggled = false;
    /// <summary>
    /// How long before this slot should be toggled.
    /// </summary>
    public float cooldownUntilToggle {private set; get;}


    


    public void OnPointerEnter(PointerEventData eventData) {
        animator.SetBool(isHovering,true);
    }
    public void OnPointerExit(PointerEventData eventData) {
        animator.SetBool(isHovering,false);

    }

    private void Update() {

        if (waitingToBeToggled) {

            cooldownUntilToggle -= Time.deltaTime;

            if (cooldownUntilToggle <= 0) {
                waitingToBeToggled = false;
                Toggle();
            }
        }
    }

    public void BeginToggleCountdown(float timeUntilToggled) {
        waitingToBeToggled = true;
        cooldownUntilToggle = timeUntilToggled;
    }

    public void CancelToggleCountdown() {
        waitingToBeToggled = false;
    }

    


    public void Toggle() {
        animator.SetTrigger(toggled);
    }

}
