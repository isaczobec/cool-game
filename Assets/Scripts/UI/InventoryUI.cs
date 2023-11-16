using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] private GameObject InventorySlotPrefab;
    [SerializeField] private GameObject inventoryParentObject;

    private List<GameObject> InventorySlotObjects = new List<GameObject>();

    [SerializeField] private int DimensionX;
    [SerializeField] private int DimensionY;

    [SerializeField] private float gridSpacingY;
    [SerializeField] private float gridSpacingX;

    private float slotScaleFactor;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }


    public void CreateGrid() {


        for (int x = 0; x < DimensionX; x++) {
            for (int y = 0; y < DimensionY; y++) {

                GameObject inventorySlot = Instantiate(InventorySlotPrefab,inventoryParentObject.transform);
                inventorySlot.transform.localPosition = new Vector3(x * gridSpacingX,y * gridSpacingY, 0f);
                InventorySlotObjects.Add(inventorySlot);

            }

        }


    }
}
