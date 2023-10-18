using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElemental : Enemy
{

    [SerializeField] float movementSpeed = 10f;

    [SerializeField] float agroDistance = 60f; // the distance within which the elemental will follow the player

    private Vector3 velocity = Vector3.zero;

    public override void HandleAI()
    {

        Vector3 positionDiffernce = player.transform.position - transform.position;


        if (positionDiffernce.magnitude < agroDistance) {
        velocity = (player.transform.position - transform.position).normalized;
        transform.position += velocity * movementSpeed * Time.deltaTime;
        } else {
            velocity = Vector3.zero;
        }


    }

}
