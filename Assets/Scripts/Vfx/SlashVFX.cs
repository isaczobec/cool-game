using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SlashVFX : MonoBehaviour
{

    /// <summary>
    /// the item that produces this slash.
    /// </summary>
    [SerializeField] private Item item;

    
    /// <summary>
    /// the bone that this slash will parent to
    /// </summary>
    [SerializeField] private Transform holdingBone;

    /// <summary>
    /// The transform the slash will be parented to after being parented to the holding bone
    /// </summary>
    [SerializeField] private Transform mainTransform;

    [SerializeField] private float scaleFactor;

    /// <summary>
    /// the visualeffect that should be played once this item slashes.
    /// </summary>
    [SerializeField] VisualEffect visualEffect;

    /// <summary>
    /// remaining time until this gameobject destroys itself
    /// </summary>
    [SerializeField] float timeUntilDestroy;
 
    private void Start() {
        Slash();
    }

    public void Slash() {

        Player player;

        if (item != null) {

            player = item.GetPlayer();
            transform.parent = player.GetHoldingBone();

        } else {
            transform.parent = holdingBone;
        }


        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = new Vector3(scaleFactor,scaleFactor,scaleFactor);


        if (item != null) {

        transform.parent = item.GetPlayer().transform;
        } else {
            transform.parent = mainTransform;
        }

        
    }

    private void Update() {
        timeUntilDestroy -= Time.deltaTime;
        if (timeUntilDestroy <= 0) {
            Destroy(gameObject);
        }
    }

}
