using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepVFX : MonoBehaviour
{

    [SerializeField] private Transform lFeetTransform;
    [SerializeField] private Transform rFeetTransform;


    [SerializeField] private GameObject FootStepVfxObject;

    [SerializeField] private Transform visualParentTransform;

    /// <summary>
    /// Creates a footstep cloud. leftFoot = true if the left foot should create a cloud. Otherwise the right foot does it.
    /// </summary>
    /// <param name="leftFoot"></param>
    public void CreateFootStepVFX(bool leftFoot) {

        

        Transform footTransform;
        if (leftFoot) {
            footTransform = lFeetTransform;
        } else {
            footTransform = rFeetTransform;
        }

        Debug.Log("Helklo there");

        GameObject footstepCloud = Instantiate(FootStepVfxObject,footTransform);
        footstepCloud.transform.localPosition = Vector3.zero;
        footstepCloud.transform.parent = null;
        footstepCloud.transform.rotation = visualParentTransform.rotation;
        footstepCloud.transform.localScale = Vector3.one;
        

    }


    
}
