using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MrGoober : Enemy
{

    [SerializeField] private InteractZone interactZone;

    [SerializeField] private Animator animator;
    
    [SerializeField] private string waveEventString = "wave";
    [SerializeField] private string waveAnimatorString = "wave";
    [SerializeField] private string gooberDialougeFinnishedString = "gooberDialougeFinnished";


    [SerializeField] private Transform visualTransform;
    [SerializeField] private Transform forwardsTransform;

    // Start is called before the first frame update
    void Start()
    {
        DialougeBubble.Instance.DialougeLineFinnished += DialougeLineFinnishedEvent;
        interactZone.InteractZoneClicked += InteractedWith;
    }

    private void InteractedWith(object sender, EventArgs e)
    {
        animator.SetTrigger(waveAnimatorString);


        StartCoroutine(FaceTowardsTransform(Player.Instance.transform,0.5f));

    }

    private void DialougeLineFinnishedEvent(object sender, string e)
    {
        if (e == waveEventString) {
            animator.SetTrigger(waveAnimatorString);
        }

        if (e == gooberDialougeFinnishedString) {
            transform.rotation = Quaternion.identity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator FaceTowardsTransform(Transform faceTowardsTransform, float duration) {

        float timePassed = 0f;

        
        Vector3 targetRotation;
        Vector3 oldRotation = visualTransform.rotation.eulerAngles;

        float rightAngle = 270f;
        float leftAngle = 90f;

        if (faceTowardsTransform.position.x > visualTransform.position.x) {
            targetRotation = new Vector3(oldRotation.x,rightAngle,oldRotation.z);
        } else {
            targetRotation = new Vector3(oldRotation.x,leftAngle,oldRotation.z);
        }
        

        while (duration > timePassed) {
            visualTransform.rotation = Quaternion.Euler(Vector3.Slerp(visualTransform.rotation.eulerAngles, targetRotation,timePassed/duration));

            timePassed += Time.deltaTime;

            yield return null;
        }



    }
}
