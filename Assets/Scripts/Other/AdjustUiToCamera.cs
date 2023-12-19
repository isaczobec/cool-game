using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustUiToCamera : MonoBehaviour
{
    
    [SerializeField] private Transform cameraTransform = null;

    private Vector3 baseScale;
    [SerializeField] private float initialDistance;


    [SerializeField] private bool shouldFaceCamera;
    [SerializeField] private bool shouldHaveConstantScale;

    private void Start() {
        if (cameraTransform == null) {
            cameraTransform = Camera.main.transform;
        }
        baseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFaceCamera) {RotateToCamera();}
        if (shouldHaveConstantScale) {AdjustToConstantSize();}
        
    }

    private void RotateToCamera() {
        transform.forward = cameraTransform.forward;
    }

    private void AdjustToConstantSize() {
        float dist = Vector3.Distance(transform.position,cameraTransform.position);
        transform.localScale = baseScale * dist / initialDistance;
    }
}
