using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElemental : Enemy
{

    [SerializeField] float movementSpeed = 10f;

    [SerializeField] float agroDistance = 60f; // the distance within which the elemental will follow the player

    private Vector3 velocity = Vector3.zero;


    [Header("Projectile references")]
    [SerializeField] private GameObject fireBallProjectilePrefab;

    /// <summary>
    /// time in seconds in between the fire elemental will fire fireballs.
    /// </summary>
    [SerializeField] private float maxFireBallCoolDown = 1;
    private float fireBallCoolDown;

    public override void HandleAI()
    {

        Vector3 positionDiffernce = player.transform.position - transform.position;


        if (positionDiffernce.magnitude < agroDistance) {
        velocity = (player.transform.position - transform.position).normalized;
        transform.position += velocity * movementSpeed * Time.deltaTime;
        } else {
            velocity = Vector3.zero;
        }


        fireBallCoolDown -= Time.deltaTime;
        if (fireBallCoolDown <= 0) {
            ShootFireBall();
            fireBallCoolDown = maxFireBallCoolDown;
        }

    }


    private void ShootFireBall() {

        GameObject projectileObject = Instantiate(fireBallProjectilePrefab);
        FireBallProjectile projectile = projectileObject.GetComponent<FireBallProjectile>();
        projectile.SetOwner(null, this);
        projectile.ownerEntity = this;
        projectile.Initialize();

    }

}
