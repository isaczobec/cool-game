using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    
    [SerializeField] private bool isPlayer;



    // enemy is null if the hurtbox belongs to a player, and vice versa
    [SerializeField, SerializeReference] private Player player;
    [SerializeField, SerializeReference] private Enemy enemy;


    /// <summary>
    /// Returns the IHittableEntity which this hitbox is attached to. 
    /// </summary>
    public IHittableEntity getOwnerEntity() {
        if (player != null) {
            return player;
        } else if (enemy != null) {
            return enemy;
        } else {
            return null;
        }
    }

   

    public bool getIsPlayer() {
        return isPlayer;
    }



}
