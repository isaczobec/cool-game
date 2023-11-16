using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that overlays an entity with a material while they have invincibility frames. 
/// </summary>
public class EntityDamageFlash : MonoBehaviour
{
    /// <summary>
    /// the material that will be displayed while the entity has invincibility frames.
    /// </summary>
    [SerializeField] private Material damagedMaterial;

    [SerializeField] private Enemy enemy;
    [SerializeField] private Player player;
    private IEntity entity;

    /// <summary>
    /// Parent object which's children will  recieve the damaged material
    /// </summary>
    [SerializeField] private GameObject entityVisualGameObject;


    private List<Material[]> storedMaterials = new List<Material[]>(); // store all materials for every object
    private List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>(); //stores references to the skinned mesh renderers


    /// <summary>
    /// Used to only call the turn on/off methods when it flips over, to increase performance.
    /// </summary>
    private bool damageOverlayOn;




    // Start is called before the first frame update
    void Start()
    {

        if (enemy != null) {
            entity = enemy;
        } else if (player != null) {
            entity = player;
        }


        foreach (Transform child in entityVisualGameObject.transform) {

            if (child.transform.TryGetComponent(out SkinnedMeshRenderer meshRenderer)) {

                skinnedMeshRenderers.Add(meshRenderer);

                storedMaterials.Add(meshRenderer.materials);

            } 


        }

    }

    void TurnOffDamageOverlay() {
        int counter = 0;
        foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers) {

            if (skinnedMeshRenderer != null) {

            skinnedMeshRenderer.materials = storedMaterials[counter];

            counter += 1;
            }

        
        }
    }

    void TurnOnDamageOverlay() {
        foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers) {
            if (skinnedMeshRenderer != null) { 

            skinnedMeshRenderer.material = damagedMaterial;
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (entity.GetInvincibilityTime() >= 0) {
            if (damageOverlayOn == false) {
            TurnOnDamageOverlay();
            damageOverlayOn = true;
            }
        } else if (damageOverlayOn == true) {
            TurnOffDamageOverlay();
            damageOverlayOn = false;
        }
    }
}
