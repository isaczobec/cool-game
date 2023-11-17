using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{

    [SerializeField] private TextMeshPro textMeshPro;

    [Header("Animation Variables")]
    [SerializeField] float initialVelocity;
    [SerializeField] float lowerVelocityRandomizationRange = 0.7f;
    [SerializeField] float maxVelAngle = 90;
    [SerializeField] float minVelAngle = 0;
    [SerializeField] float velocityDamping;
    [SerializeField] float randomSpawnLocationRadius;


    [SerializeField] private Color32 enemyColor;
    [SerializeField] private float enemyAlpha = 0.5f;
    [SerializeField] private Color32 playerColor;
    /// <summary>
    /// The amount the scale is multiplied by if the player is hit (intended to alert them)
    /// </summary>
    [SerializeField] private float playerScaleFactor = 1.7f;
    

    private Vector3 velocity = Vector3.zero;

    public void Setup(HitInfo hitInfo) {
        textMeshPro.text = hitInfo.damage.ToString();

        float randomAngle = Random.Range(minVelAngle * Mathf.Deg2Rad,maxVelAngle * Mathf.Deg2Rad);
        velocity = new Vector3(Mathf.Sin(randomAngle),Mathf.Cos(randomAngle),0f) * Random.Range(lowerVelocityRandomizationRange,1f) * initialVelocity;
        transform.position += new Vector3(randomSpawnLocationRadius * Random.Range(0f,1f),randomSpawnLocationRadius * Random.Range(0f,1f),0f); // randomizes the number's spawn location.
        transform.localScale *= hitInfo.damage/hitInfo.baseDamage; //change the scale based on the damage variance


        if (hitInfo.hurtEntity.GetType() == typeof(Player)) {
        textMeshPro.color = playerColor;
        transform.localScale *= playerScaleFactor;
        } else {
        textMeshPro.color = enemyColor;

        textMeshPro.alpha = enemyAlpha;
        }


    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
        transform.position += velocity * Time.deltaTime;
        velocity = Vector3.Lerp(velocity,Vector3.zero, Time.deltaTime * velocityDamping);
    }
}
