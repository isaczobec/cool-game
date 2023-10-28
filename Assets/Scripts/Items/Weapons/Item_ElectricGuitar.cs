using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_ElectricGuitar : Item
{

    [SerializeField] private GameObject noteProjectilePrefab; // the prefab of the projectile the guitar fires


    public override void PrimaryUse()
    {
        GameObject projectileObject = Instantiate(noteProjectilePrefab);
        NoteProjectile projectile = projectileObject.GetComponent<NoteProjectile>();
        projectile.SetOwner(Player.Instance);
        projectile.parentItem = this;
        projectile.Initialize();
        
    }

}
