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

        Player player = item.GetPlayer();

        transform.parent = player.GetHoldingBone();

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = new Vector3(scaleFactor,scaleFactor,scaleFactor);

        transform.parent = player.transform;


    }

    private void Update() {
        timeUntilDestroy -= Time.deltaTime;
        if (timeUntilDestroy <= 0) {
            Destroy(gameObject);
        }
    }

}
