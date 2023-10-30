using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stats associated with every instance of an Item. Every item should have a reference to a unique one.
/// </summary>
public class ItemData : MonoBehaviour
{

    /// <summary>
    /// the ID used to identify the item, crucial that it is written exactly the same everywhere
    /// </summary>
    public string itemID;

    /// <summary>
    /// the name of the item that is shown to the player
    /// </summary>
    public string displayName;

    /// <summary>
    /// the amount of damage this item does, provided its a weapon
    /// </summary>
    public float damage;

    /// <summary>
    /// The percent of how much the damage of this item (Usually) varies (Randomized).
    /// </summary>
    public float damageVariance;
    
}
