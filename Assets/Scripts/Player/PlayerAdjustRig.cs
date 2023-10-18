using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdjustRig : MonoBehaviour
{

    [SerializeField] private Transform spineTransform;
    [SerializeField] private MouseInfo mouseInfo;

    [SerializeField] private Player player;


    //the amount of the degrees the spine can be rotated
    [SerializeField] float maxSpineRotation = 30f;
    [SerializeField] float minSpineRotation = -30;
    

    [SerializeField] private float blendSpineRotationSpeed = 4; // how fast the spine rotation gets blended in when the player starts/stops attacking



    

    

    private void LateUpdate()
    {

        UpdatePlayerSpineRotation();

    }

    void UpdatePlayerSpineRotation() {

      
        if (player.GetPlayerIsAttacking()) {
            
            Vector3 spineOldTransform = spineTransform.rotation.eulerAngles;
            float newSpineRotation = SmoothFunction(mouseInfo.GetPlayerMouseAngle(),minSpineRotation,maxSpineRotation);

            spineTransform.rotation = Quaternion.Euler(new Vector3(newSpineRotation,spineOldTransform.y,spineOldTransform.z));
            
        }





    }

    float SmoothFunction(float value, float min, float max) {


        if (value > min) {
            if (value < max) {

                float intervalLength = max - min;
                float percentOfInterval = (value-min)/intervalLength;
                

                return (Mathf.Pow(percentOfInterval,2) * 3 - Mathf.Pow(percentOfInterval,3) * 2) * intervalLength + min;
            } else {
                return max;
            }
        }   else {
            return min;
        }

    }

}
