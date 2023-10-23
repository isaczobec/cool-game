using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Parernt class of all items. Contains methods for using the item, etc
/// </summary>
public class Item : MonoBehaviour
{

    [SerializeField] public GameObject visualObject; // the visualObject associated with this item
    
    [SerializeField] public Player player; // the player that is holding this item

    /// <summary>
    /// Called when the player uses this item with their primary action.
    /// </summary>
    public virtual void PrimaryUse() {

    }
    /// <summary>
    /// Called when the player uses this item with their secondary action.
    /// </summary>
    public virtual void SecondaryUse() {

    }

}
