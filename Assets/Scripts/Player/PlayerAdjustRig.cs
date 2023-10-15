using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdjustRig : MonoBehaviour
{

    [SerializeField] private Transform spineTransform;
    [SerializeField] private MouseInfo mouseInfo;

    

    // Update is called once per frame
    void LateUpdate()
    {


    }

    void UpdatePlayerSpineRotation() {
        Vector3 spineOldTransform = spineTransform.rotation.eulerAngles;
        
        spineTransform.rotation = Quaternion.Euler(new Vector3(mouseInfo.GetPlayerMouseAngle(),spineOldTransform.y,spineOldTransform.z));
    }

}
