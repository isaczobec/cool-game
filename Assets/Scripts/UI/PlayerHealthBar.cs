using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private Material healthBarMaterial;

    [SerializeField] private Animator animator;
    
    [SerializeField] string takeDamageReferenceString = "TakeDamage";
    [SerializeField] string healthAlertSliderReferenceString = "_HealthAlertSlider";

    [SerializeField] string healthSliderReferenceString = "_HealthSlider";
    [SerializeField] string voronoiStrengthReferenceString = "_VoronoiStrength";
    [SerializeField] float voronoiStrength = 1;

    [SerializeField] private float maxAlertHealthBarLevel;
    [SerializeField] private float minAlertHealthBarLevel;

    [SerializeField] private float maxHealthBarLevel;
    [SerializeField] private float minHealthBarLevel;

    /// <summary>
    /// How long in seconds after taking damage the health bar will begin ticking down.
    /// </summary>
    [SerializeField] private float healthTickDownDelay;
    private float healthTickDownCooldown;
    

    /// <summary>
    /// The speed factor with which the health ticks down.
    /// </summary>
    [SerializeField] private float healthTickDownSpeed;

    private float currentHealthLevel;
    private float targetHealthLevel;


    [SerializeField] private TextMeshProUGUI textMeshPro;

    private float currentHealthDisplayNumber;
    [SerializeField] private float HealthNumberTickDownSpeed = 8f;


    

    // Start is called before the first frame update
    void Start()
    {
        player.OnPlayerGotHit += Player_OnPlayerGotHit;
        healthBarMaterial.SetVector(healthAlertSliderReferenceString,new Vector2(maxAlertHealthBarLevel,0));
        healthBarMaterial.SetVector(healthSliderReferenceString,new Vector2(maxHealthBarLevel,0));

        currentHealthLevel = maxHealthBarLevel;
        targetHealthLevel = maxHealthBarLevel;

        healthTickDownCooldown = healthTickDownDelay;


        textMeshPro.text = player.maxHealth.ToString() + "/" + player.maxHealth.ToString();


        currentHealthDisplayNumber = player.maxHealth;
    }

    private void Player_OnPlayerGotHit(object sender, HitInfo e)
    {
        float healthPercent = player.GetHealthPercent();

        
        // set the alert level instantly when taken damage
        float AlertHealthLevel = (maxAlertHealthBarLevel - minAlertHealthBarLevel) * healthPercent + minAlertHealthBarLevel;  

        healthBarMaterial.SetVector(healthAlertSliderReferenceString,new Vector2(AlertHealthLevel,0));

        targetHealthLevel = (maxHealthBarLevel - minHealthBarLevel) * healthPercent + minHealthBarLevel;

        healthTickDownCooldown = healthTickDownDelay;

        animator.SetTrigger(takeDamageReferenceString);


    }

    // Update is called once per frame
    private void Update()
    {
        healthTickDownCooldown -= Time.deltaTime;

        if (healthTickDownCooldown <= 0) {
            currentHealthLevel = Mathf.Lerp(currentHealthLevel, targetHealthLevel, Time.deltaTime * healthTickDownSpeed);
            healthBarMaterial.SetVector(healthSliderReferenceString,new Vector2(currentHealthLevel,0));

            
        }


        healthBarMaterial.SetFloat(voronoiStrengthReferenceString,voronoiStrength);


        currentHealthDisplayNumber = Mathf.Lerp(currentHealthDisplayNumber, player.health, Time.deltaTime * HealthNumberTickDownSpeed);
        textMeshPro.text = Mathf.Round(currentHealthDisplayNumber).ToString() + "/" + player.maxHealth.ToString();
    }
}
