using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField] private GameObject InventorySlotPrefab;
    [SerializeField] private GameObject inventoryParentObject;

    /// <summary>
    /// Transform whichs position is added onto every inventory slot
    /// </summary>
    [SerializeField] private Transform slotsOffsetTransform;

    [Header("Animation variables")]
    [SerializeField] private Animator animator;

    [SerializeField] private string inventoryToggled = "inventoryToggled";

    [SerializeField] private float toggleTimeDifferenceBetweenSlots = 0.01f;


    private List<GameObject> InventorySlotObjects = new List<GameObject>();
    private List<InventorySlot> InventorySlots = new List<InventorySlot>();

    [Header("Dimension Settings")]
    [SerializeField] private int DimensionX;
    [SerializeField] private int DimensionY;

    [SerializeField] private float gridSpacingY;
    [SerializeField] private float gridSpacingX;

    private float slotScaleFactor;

    // Start is called before the first frame update
    void Start()
    {

        PlayerInputHandler.Instance.inventoryToggled += ToggleInventory;

        CreateGrid();

        

    }


    public void CreateGrid() {


        for (int x = 0; x < DimensionX; x++) {
            for (int y = 0; y < DimensionY; y++) {

                GameObject inventorySlot = Instantiate(InventorySlotPrefab,inventoryParentObject.transform);
                inventorySlot.transform.localPosition = new Vector3(x * gridSpacingX + slotsOffsetTransform.localPosition.x,y * gridSpacingY + slotsOffsetTransform.localPosition.y, 0f);
                InventorySlotObjects.Add(inventorySlot);

                InventorySlots.Add(inventorySlot.GetComponent<InventorySlot>());

            }

        }


    }


    private void ToggleInventory(object sender, EventArgs e)
    {
        Debug.Log("B");
        animator.SetTrigger(inventoryToggled);

        float cooldownTime = 1f;
        foreach (InventorySlot invSlot in InventorySlots) {

            if (!invSlot.waitingToBeToggled) {

            invSlot.BeginToggleCountdown(cooldownTime * toggleTimeDifferenceBetweenSlots);
            } else {
                invSlot.CancelToggleCountdown();
            }



            cooldownTime += 1f;
        }

    }

}
