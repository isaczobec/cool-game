using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Parernt class of all items. Contains methods for using the item, etc
/// </summary>
public class Item : MonoBehaviour
{

    [SerializeField] private ItemData itemData;

    private Player player; // the player that is holding this item

    [SerializeField] private GameObject visualObject; // the visualObject associated with this item
    
    /// <summary>
    /// The point in space which the player should be holding this object at.
    /// </summary>
    [SerializeField] private Transform holdingTransform; 


    /// <summary>
    /// the audiomanager associated with this item
    /// </summary>
    [SerializeField] public AudioManager audioManager;
    /// <summary>
    /// the name of the usesound of this item. "UseSound" is the default.
    /// </summary>
    [SerializeField] public string useSoundName = "UseSound";
    

    

    /// <summary>
    /// Called when the player uses this item with their primary action.
    /// </summary>
    public virtual void PrimaryUse() {
        audioManager.Play(useSoundName,Random.Range(0.8f,1.2f));

    }
    /// <summary>
    /// Called when the player uses this item with their secondary action.
    /// </summary>
    public virtual void SecondaryUse() {

    }


    /// <summary>
    /// Get the transform this item should be held at.
    /// </summary>
    /// <returns></returns>
    public Transform GetHoldingTransform() {
        return holdingTransform;
    }

    public void SetPlayer(Player player) {
        this.player = player;
    }

    public Player GetPlayer() {
        return player;
    }

    public ItemData GetItemData() {
        return itemData;
    }

}
