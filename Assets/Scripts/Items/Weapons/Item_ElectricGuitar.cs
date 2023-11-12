using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_ElectricGuitar : Item
{

    [SerializeField] private GameObject noteProjectilePrefab; // the prefab of the projectile the guitar fires

    [SerializeField] private SlashVFX slashVFX;


    public override void PrimaryUse()
    {

        // shoot a note projectile
        GameObject projectileObject = Instantiate(noteProjectilePrefab);
        NoteProjectile projectile = projectileObject.GetComponent<NoteProjectile>();
        projectile.SetOwner(Player.Instance);
        projectile.parentItem = this;
        projectile.Initialize();

        // play the slash vfx when this item is used
        slashVFX.Slash();
        
    }

}
