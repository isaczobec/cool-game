using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OrbitSwordProjectile : Projectile
{

    private Vector3 orbitCenter;
    private float distanceFromOrbitcenter;

    private float angleOffset;

    [SerializeField] private float moveSpeedMultiplier = 1f;
    private float moveSpeed;


    [SerializeField] private Transform visualObjectTransform;



    public override void UpdateProjectile()
    {
        HandleOrbiting();
    }

    public void InitializeProjectile (Vector3 orbitPosition, float distanceFromOrbitcenter, float angleOffset,float moveSpeed)
    {

        
        orbitCenter = orbitPosition;
        this.moveSpeed = moveSpeed;
        this.distanceFromOrbitcenter = distanceFromOrbitcenter;
        this.distanceFromOrbitcenter = distanceFromOrbitcenter;
        this.angleOffset = angleOffset;

    }

    private void HandleOrbiting()
    {
        float x = math.cos(angleOffset);
        float y = math.sin(angleOffset);

        Vector3 newPosition = new Vector3(x * distanceFromOrbitcenter, y * distanceFromOrbitcenter, 0f) + orbitCenter;
        
        transform.position = newPosition;

        angleOffset += Time.deltaTime * moveSpeed * moveSpeedMultiplier;

        HandleVisualObjectRotation();
    }


    private void HandleVisualObjectRotation() {
        visualObjectTransform.rotation = Quaternion.Euler(0,0,Mathf.Rad2Deg * angleOffset - 90f);
    }

    



}
