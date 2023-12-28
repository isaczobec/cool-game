using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGotoPoint : MonoBehaviour
{

    private Transform gotoTransform;
    [SerializeField] private float cameraTargetFov;

    // Start is called before the first frame update
    void Awake()
    {
        gotoTransform = transform;
    }

    public Transform GetGotoTransform() {
        return gotoTransform;
    }
    public float GetTargetFov() {
        return cameraTargetFov;
    }

}
