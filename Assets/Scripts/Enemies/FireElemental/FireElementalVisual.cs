using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElementalVisual : EnemyVisual
{
    [Header("Visual Variables")]
    [SerializeField] float spinAroundSpeed = 5f;
    [SerializeField] float minSpinLerpAngle = 1f;
    [SerializeField] float rightSpinLerpAngle = 135f;
    [SerializeField] float leftSpinLerpAngle = 225f;
    
    void Update()
    {
        FacePlayer(spinAroundSpeed,minSpinLerpAngle,rightSpinLerpAngle,leftSpinLerpAngle);
    }
    
}
