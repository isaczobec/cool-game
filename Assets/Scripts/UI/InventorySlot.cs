using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private Animator animator;

    [SerializeField] private string isHovering = "isHovered";


    


    public void OnPointerEnter(PointerEventData eventData) {
        animator.SetBool(isHovering,true);
    }
    public void OnPointerExit(PointerEventData eventData) {
        animator.SetBool(isHovering,false);

    }
}
